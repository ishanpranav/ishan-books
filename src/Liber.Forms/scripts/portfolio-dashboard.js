// portfolio-dashboard.js 
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved. 
// Licensed under the MIT License. 

const report = window.chrome.webview.hostObjects.report;

let holdings = [];

const columns = [
    { key: 'symbol', label: 'Symbol' },
    { key: 'price', label: 'Price' },
    { key: 'quantity', label: 'Quantity' },
    { key: 'cost', label: 'Average cost' },
    { key: 'marketValue', label: 'Market value' },
    { key: 'commission', label: 'Commissions' },
    { key: 'dividend', label: 'Dividends' },
    { key: 'realizedGain', label: 'Realized gain' },
    { key: 'unrealizedGain', label: 'Unrealized gain' },
    { key: 'totalGain', label: 'Total gain' },
    { key: 'totalReturn', label: 'Total return %' },
    { key: 'trend', label: 'Trend' }
];

let sortColumn = 'marketValue';
let sortDirection = -1;
let trendChart = null;
let donutChart = null;
let sparklineCharts = [];

function formatNumber(value, digits = 2) {
    return value.toLocaleString(undefined, {
        minimumFractionDigits: digits,
        maximumFractionDigits: digits
    });
}

function formatSigned(value) {
    if (value > 0) {
        return '+' + formatNumber(value);
    }

    return formatNumber(value);
}

function textColorClass(value) {
    if (value == 0) {
        return 'text-secondary';
    }

    if (value < 0) {
        return 'text-danger';
    }

    return 'text-success';
}

function getThemeColor(variableName) {
    const styles = getComputedStyle(document.documentElement);

    return styles.getPropertyValue(variableName).trim();
}

function percentOf(part, whole) {
    if (whole === 0) {
        return 0;
    }

    return part / whole * 100;
}

function toTimestamp(date) {
    if (typeof date === 'string' && date.length === 10) {
        return new Date(date + 'T00:00:00').getTime();
    }

    return new Date(date).getTime();
}

function calculateRows() {
    const rows = [];

    for (let i = 0; i < holdings.length; i++) {
        const h = holdings[i];
        const timeSeries = h.timeSeries || []; 

        rows.push({
            symbol: h.symbol,
            name: h.name || h.symbol,
            price: h.position.price || 0,
            quantity: h.position.quantity,
            cost: h.position.cost,
            marketValue: h.position.marketValue,
            unrealizedGain: h.position.unrealizedGain || 0,
            commission: h.position.commission || 0,
            dividend: h.position.dividend || 0,
            realizedGain: h.position.realizedGain || 0,
            totalGain: h.position.totalGain || 0,
            totalReturn: percentOf(h.position.totalGain || 0, h.position.costBasis || 0),
            costBasis: h.position.costBasis || 0,
            timeSeries: timeSeries,
            labels: h.labels || [],
            trendIsUp: timeSeries.length > 1 && timeSeries[timeSeries.length - 1] - timeSeries[0] > 0,
            color: h.color
        });
    }

    return rows;
}

function sortRows(rows) {
    rows.sort((a, b) => {
        let result;

        if (typeof a[sortColumn] === 'string') {
            result = a[sortColumn].localeCompare(b[sortColumn]);
        } else {
            result = a[sortColumn] - b[sortColumn];
        }

        return result * sortDirection;
    });
}

function calculateTotals(rows) {
    const totals = {
        marketValue: 0,
        costBasis: 0,
        unrealizedGain: 0,
        commission: 0,
        dividend: 0,
        realizedGain: 0,
        totalGain: 0
    };

    for (let i = 0; i < rows.length; i++) {
        totals.marketValue += rows[i].marketValue;
        totals.costBasis += rows[i].costBasis;
        totals.unrealizedGain += rows[i].unrealizedGain;
        totals.commission += rows[i].commission;
        totals.dividend += rows[i].dividend;
        totals.realizedGain += rows[i].realizedGain;
        totals.totalGain += rows[i].totalGain;
    }

    totals.totalReturn = percentOf(totals.totalGain, totals.costBasis);

    return totals;
}

function drawTableHeader() {
    let html = '';

    for (let i = 0; i < columns.length; i++) {
        const column = columns[i];

        let sortAttribute = '';
        if (column.key === sortColumn) {
            if (sortDirection === 1) {
                sortAttribute = 'aria-sort="ascending"';
            } else {
                sortAttribute = 'aria-sort="descending"';
            }
        }

        html += `<th data-key='${column.key}' ${sortAttribute} tabindex='0' scope='col' class="text-center align-middle fw-semibold small text-body-secondary">${column.label}</th>`;
    }

    document.getElementById('head').innerHTML = html;
}

function drawTableBody(rows) {
    let html = '';

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];

        html += `
            <tr>
                <td>
                    <span class='fw-bold text-primary'>${row.symbol}</span>
                    <span class='d-block small text-body-secondary text-truncate' style='max-width:190px'>${row.name}</span>
                </td>
                <td class="text-end">${formatNumber(row.price)}</td>
                <td class="text-end">${formatNumber(row.quantity, 4)}</td>
                <td class="text-end">${formatNumber(row.cost)}</td>
                <td class="text-end"><b>${formatNumber(row.marketValue)}</b></td>
                <td class='text-end ${textColorClass(-row.commission)}'>${formatNumber(row.commission)}</td>
                <td class='text-end ${textColorClass(row.dividend)}'>${formatSigned(row.dividend)}</td>
                <td class='text-end ${textColorClass(row.realizedGain)}'>${formatSigned(row.realizedGain)}</td>
                <td class='text-end ${textColorClass(row.unrealizedGain)}'>${formatSigned(row.unrealizedGain)}</td>
                <td class='text-end ${textColorClass(row.totalGain)}'>${formatSigned(row.totalGain)}</td>
                <td class='text-end ${textColorClass(row.totalReturn)}'>${formatSigned(row.totalReturn)}%</td>
                <td>
                    <div class="portfolio-dashboard-spark">
                        <canvas id='spark-${row.symbol}' aria-hidden='true'></canvas>
                    </div>
                </td>
            </tr>`;
    }

    document.getElementById('body').innerHTML = html;
}

function drawTableFooter(totals) {
    document.getElementById('foot').innerHTML = `
        <tr>
            <td>Total</td>
            <td></td><td></td><td></td>
            <td class="text-end">${formatNumber(totals.marketValue)}</td>
            <td class='text-end ${textColorClass(-totals.commission)}'>${formatNumber(totals.commission)}</td>
            <td class='text-end ${textColorClass(totals.dividend)}'>${formatSigned(totals.dividend)}</td>
            <td class='text-end ${textColorClass(totals.realizedGain)}'>${formatSigned(totals.realizedGain)}</td>
            <td class='text-end ${textColorClass(totals.unrealizedGain)}'>${formatSigned(totals.unrealizedGain)}</td>
            <td class='text-end ${textColorClass(totals.totalGain)}'>${formatSigned(totals.totalGain)}</td>
            <td class='text-end ${textColorClass(totals.totalGain)}'>${formatSigned(totals.totalReturn)}%</td>
            <td></td>
        </tr>`;
}

function drawSummary(rows, totals) {
    document.getElementById('market-value').textContent = formatNumber(totals.marketValue);

    document.getElementById('tg').innerHTML = `
        <span class='${textColorClass(totals.totalGain)}'>
            ${formatSigned(totals.totalGain)} (${formatSigned(totals.totalReturn)}%)
        </span>`;

    document.getElementById('cb').textContent = formatNumber(totals.costBasis);
    document.getElementById('cnt').textContent = rows.length + ' positions';
}

function drawTrendChart(rows) {
    const dateSet = new Set();

    for (let i = 0; i < rows.length; i++) {
        for (let j = 0; j < rows[i].labels.length; j++) {
            dateSet.add(toTimestamp(rows[i].labels[j]));
        }
    }

    const dates = Array.from(dateSet).sort((a, b) => a - b);
    const priceMaps = [];

    for (let i = 0; i < rows.length; i++) {
        const priceByDate = new Map();

        for (let j = 0; j < rows[i].labels.length; j++) {
            priceByDate.set(toTimestamp(rows[i].labels[j]), rows[i].timeSeries[j]);
        }

        priceMaps.push(priceByDate);
    }

    const lastPrices = new Array(rows.length).fill(undefined);
    const points = []; 

    for (let d = 0; d < dates.length; d++) {
        let total = 0;

        for (let i = 0; i < rows.length; i++) {
            if (priceMaps[i].has(dates[d])) {
                lastPrices[i] = priceMaps[i].get(dates[d]);
            }

            if (lastPrices[i] !== undefined) {
                total += lastPrices[i] * rows[i].quantity;
            }
        }

        points.push({ x: dates[d], y: total });
    }

    const endedHigher = points.length > 0 && points[points.length - 1].y >= points[0].y;
    const lineColor = getThemeColor(endedHigher ? '--bs-success' : '--bs-danger');
    const textColor = getThemeColor('--bs-secondary-color');
    const gridColor = getThemeColor('--bs-border-color');

    if (trendChart) {
        trendChart.destroy();
    }

    trendChart = new Chart(document.getElementById('main'), {
        type: 'line',
        data: {
            datasets: [{
                data: points,
                borderColor: lineColor,
                backgroundColor: lineColor + '1f',
                fill: true,
                borderWidth: 2,
                pointRadius: 0,
                pointHoverRadius: 4,
                tension: 0.25
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: { mode: 'index', intersect: false },
            plugins: {
                legend: { display: false },
                tooltip: {
                    displayColors: false,
                    callbacks: {
                        title: (items) => {
                            return new Date(items[0].parsed.x).toLocaleDateString(undefined, { dateStyle: 'medium' });
                        },
                        label: (context) => {
                            return formatNumber(context.parsed.y);
                        }
                    }
                }
            },
            scales: {
                x: {
                    type: 'time',
                    grid: { display: false },
                    ticks: { color: textColor, maxTicksLimit: 6, maxRotation: 0 }
                },
                y: {
                    grid: { color: gridColor },
                    ticks: {
                        color: textColor,
                        maxTicksLimit: 4
                    }
                }
            }
        }
    });
}

function drawDonutChart(rows, totals) {
    const slices = rows.filter((row) => {
        return row.marketValue > 0;
    });

    slices.sort(function (a, b) {
        return b.marketValue - a.marketValue;
    });

    const names = [];
    const values = [];
    const colors = [];

    for (let i = 0; i < slices.length; i++) {
        names.push(slices[i].symbol);
        values.push(percentOf(slices[i].marketValue, totals.marketValue));
        colors.push(slices[i].color);
    }

    if (donutChart) {
        donutChart.destroy();
    }

    donutChart = new Chart(document.getElementById('donut'), {
        type: 'doughnut',
        data: normalizeChartData({
            labels: names,
            datasets: [{
                data: values,
                backgroundColor: colors,
                borderColor: getThemeColor('--bs-body-bg'),
                borderWidth: 2
            }]
        }),
        options: {
            cutout: '60%',
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            return ' ' + context.label + ': ' + formatNumber(context.parsed, 2) + '%';
                        }
                    }
                }
            }
        }
    });
}

function drawSparklines(rows) {
    for (let i = 0; i < sparklineCharts.length; i++) {
        sparklineCharts[i].destroy();
    }

    sparklineCharts = [];

    let earliest = Infinity;
    let latest = -Infinity;

    for (let i = 0; i < rows.length; i++) {
        for (let j = 0; j < rows[i].labels.length; j++) {
            const time = toTimestamp(rows[i].labels[j]);

            earliest = Math.min(earliest, time);
            latest = Math.max(latest, time);
        }
    }

    const hasRange = earliest <= latest;

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        const canvas = document.getElementById('spark-' + row.symbol);
        const points = [];

        for (let j = 0; j < row.timeSeries.length; j++) {
            points.push({ x: toTimestamp(row.labels[j]), y: row.timeSeries[j] });
        }

        const color = getThemeColor(row.trendIsUp ? '--bs-success' : '--bs-danger');

        const chart = new Chart(canvas, {
            type: 'line',
            data: {
                datasets: [{
                    data: points,
                    borderColor: color,
                    borderWidth: 1.6,
                    pointRadius: 0,
                    tension: 0.3
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                animation: false,
                events: [],
                plugins: { legend: { display: false }, tooltip: { enabled: false } },
                scales: {
                    x: {
                        type: 'time',
                        display: false,
                        min: hasRange ? earliest : undefined,
                        max: hasRange ? latest : undefined
                    },
                    y: { display: false }
                }
            }
        });

        sparklineCharts.push(chart);
    }
}

function drawAll() {
    const rows = calculateRows();

    sortRows(rows);

    const totals = calculateTotals(rows);

    drawTableHeader();
    drawTableBody(rows);
    drawTableFooter(totals);
    drawSparklines(rows);
    drawSummary(rows, totals);
    drawTrendChart(rows);
    drawDonutChart(rows, totals);
}

async function main() {
    document.getElementById('title').innerText = await report.title;

    holdings = JSON.parse(await report.getPositions);

    document.getElementById('head').addEventListener('click', (e) => {
        const th = e.target.closest('th');

        if (!th) {
            return;
        }

        const clickedColumn = th.dataset.key;

        if (clickedColumn === 'trend') {
            return;
        }

        if (clickedColumn === sortColumn) {
            sortDirection = -sortDirection;
        } else {
            sortColumn = clickedColumn;
            if (clickedColumn === 'symbol') {
                sortDirection = 1;
            } else {
                sortDirection = -1;
            }
        }

        drawAll();
    });

    document.getElementById('head').addEventListener('keydown', (e) => {
        if (e.key === 'Enter') {
            e.target.click();
        }
    });

    drawAll();
}

main();
