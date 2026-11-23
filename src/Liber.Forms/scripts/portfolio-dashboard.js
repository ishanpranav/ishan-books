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
    { key: 'unrealizedGain', label: 'Unrealized gain' },
    { key: 'commission', label: 'Commission' },
    { key: 'realizedGain', label: 'Realized gain' },
    { key: 'totalGain', label: 'Total return' },
    { key: 'totalReturn', label: 'Total %' },
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
    if (value >= 0) {
        return '+' + formatNumber(value);
    }

    return formatNumber(value);
}

function textColorClass(value) {
    if (value >= 0) {
        return 'text-success';
    }

    return 'text-danger';
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

// 'YYYY-MM-DD' on its own is read as UTC midnight, which can display as the
// previous day in US time zones. Adding a time makes JavaScript read it as local.
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
        let price = h.price || 0;

        if (timeSeries.length > 0) {
            price = timeSeries[timeSeries.length - 1];
        }

        let startPrice = price;

        if (timeSeries.length > 0) {
            startPrice = timeSeries[0];
        }

        const commission = h.commission || 0;
        const realizedGain = h.realizedGain || 0;
        const costBasis = h.cost * h.quantity;
        const unrealizedGain = (price - h.cost) * h.quantity;
        const totalGain = unrealizedGain + realizedGain - commission;

        rows.push({
            symbol: h.symbol,
            name: h.name || h.symbol,
            sector: h.sector || h.name || h.symbol,
            price: price,
            quantity: h.quantity,
            cost: h.cost,
            marketValue: price * h.quantity,
            unrealizedGain: unrealizedGain,
            commission: commission,
            realizedGain: realizedGain,
            totalGain: totalGain,
            totalReturn: percentOf(totalGain, costBasis),
            costBasis: costBasis,
            timeSeries: timeSeries,
            labels: h.labels || [],
            trendIsUp: price >= startPrice
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
        realizedGain: 0,
        totalGain: 0
    };

    for (let i = 0; i < rows.length; i++) {
        totals.marketValue += rows[i].marketValue;
        totals.costBasis += rows[i].costBasis;
        totals.unrealizedGain += rows[i].unrealizedGain;
        totals.commission += rows[i].commission;
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
                <td class='text-end ${textColorClass(row.unrealizedGain)}'>${formatSigned(row.unrealizedGain)}</td>
                <td class="text-end">${formatNumber(row.commission)}</td>
                <td class='text-end ${textColorClass(row.realizedGain)}'>${formatSigned(row.realizedGain)}</td>
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
            <td class='text-end ${textColorClass(totals.unrealizedGain)}'>${formatSigned(totals.unrealizedGain)}</td>
            <td class="text-end">${formatNumber(totals.commission)}</td>
            <td class='text-end ${textColorClass(totals.realizedGain)}'>${formatSigned(totals.realizedGain)}</td>
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
    // 1. Collect every distinct date across all holdings, oldest first.
    const dateSet = new Set();

    for (let i = 0; i < rows.length; i++) {
        for (let j = 0; j < rows[i].labels.length; j++) {
            dateSet.add(toTimestamp(rows[i].labels[j]));
        }
    }

    const dates = Array.from(dateSet).sort((a, b) => a - b);

    // 2. For each holding, make a lookup: date -> price.
    const priceMaps = [];

    for (let i = 0; i < rows.length; i++) {
        const priceByDate = new Map();

        for (let j = 0; j < rows[i].labels.length; j++) {
            priceByDate.set(toTimestamp(rows[i].labels[j]), rows[i].timeSeries[j]);
        }

        priceMaps.push(priceByDate);
    }

    // 3. For each date, add up price x quantity across holdings.
    // If a holding has no price on a date, reuse its last known price.
    const lastPrices = new Array(rows.length).fill(undefined);
    const points = []; // each point is { x: date, y: portfolio value }

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
                data: points, // no labels array: x comes from each point
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
                    type: 'time', // Chart.js chooses the tick dates and format
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
    const valueBySector = {};

    for (let i = 0; i < rows.length; i++) {
        const sector = rows[i].sector;
        if (valueBySector[sector] === undefined) {
            valueBySector[sector] = 0;
        }
        valueBySector[sector] += rows[i].marketValue;
    }

    const sectors = [];

    for (const sectorName in valueBySector) {
        sectors.push({ name: sectorName, value: valueBySector[sectorName] });
    }

    sectors.sort(function (a, b) {
        return b.value - a.value;
    });

    const names = [];
    const values = [];
    // const colors = [];

    for (let i = 0; i < sectors.length; i++) {
        names.push(sectors[i].name);
        values.push(percentOf(sectors[i].value, totals.marketValue));
        // colors.push(sectorColors[i % sectorColors.length]);
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
                // backgroundColor: colors,
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
