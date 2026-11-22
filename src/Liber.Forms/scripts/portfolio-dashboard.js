// portfolio-dashboard.js 
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved. 
// Licensed under the MIT License. 

const report = window.chrome.webview.hostObjects.report;

let holdings = [];
// const holdings = [
//     { symbol: "AAPL", name: "Apple Inc.", sector: "Technology", price: 228.40, previousPrice: 226.10, quantity: 40, cost: 171.20 },
//     { symbol: "MSFT", name: "Microsoft Corporation", sector: "Technology", price: 431.75, previousPrice: 434.20, quantity: 25, cost: 310.00 },
//     { symbol: "NVDA", name: "NVIDIA Corporation", sector: "Technology", price: 142.30, previousPrice: 138.55, quantity: 120, cost: 61.40 },
//     { symbol: "AMZN", name: "Amazon.com, Inc.", sector: "Consumer", price: 198.60, previousPrice: 199.05, quantity: 35, cost: 142.80 },
//     { symbol: "JPM", name: "JPMorgan Chase & Co.", sector: "Financials", price: 236.90, previousPrice: 233.40, quantity: 30, cost: 168.50 },
//     { symbol: "JNJ", name: "Johnson & Johnson", sector: "Healthcare", price: 157.20, previousPrice: 158.30, quantity: 28, cost: 164.10 },
//     { symbol: "XOM", name: "Exxon Mobil Corporation", sector: "Energy", price: 114.80, previousPrice: 116.20, quantity: 45, cost: 98.60 },
//     { symbol: "VTI", name: "Vanguard Total Stock Market ETF", sector: "Funds", price: 287.10, previousPrice: 285.90, quantity: 50, cost: 219.30 }
// ];

// The table columns. "key" matches a property of each row (see Section 5).
const columns = [
    { key: "symbol", label: "Symbol" },
    { key: "price", label: "Last price" },
    { key: "change", label: "Change" },
    { key: "changePercent", label: "% change" },
    { key: "quantity", label: "quantity" },
    { key: "cost", label: "Avg cost" },
    { key: "marketValue", label: "Market value" },
    { key: "dayGain", label: "Day gain" },
    { key: "totalGain", label: "Total gain" },
    { key: "totalGainPercent", label: "Total %" },
    { key: "trend", label: "Trend" }
];

// Colors for the donut chart slices (one per sector).
const sectorColors = ["#6001d2", "#1c9ad6", "#e8a31a", "#188a4a", "#d6283a", "#7a8794", "#c24ba5"];


/* ==========================================================================
   SECTION 2: THE PAGE'S CURRENT SETTINGS ("state")
   These change when the user clicks things. After a change we redraw.
   ========================================================================== */

let selectedDays = 30;          // chart range: 7, 30 or 90 days
let sortColumn = "marketValue"; // which column the table is sorted by
let sortDirection = -1;         // 1 = ascending (small to large), -1 = descending

// We remember the charts we create so we can delete the old ones before
// drawing new ones (Chart.js complains if two charts share one canvas).
let trendChart = null;
let donutChart = null;
let sparklineCharts = [];


/* ==========================================================================
   SECTION 3: SMALL HELPER FUNCTIONS
   ========================================================================== */

// 1234.5 becomes "$1,234.50". "digits" is how many decimal places to show.
function formatDollars(value, digits = 2) {
    return value.toLocaleString("en-US", {
        style: "currency",
        currency: "USD",
        minimumFractionDigits: digits,
        maximumFractionDigits: digits
    });
}

// 1234.5 becomes "1,234.50" (no dollar sign).
function formatNumber(value, digits = 2) {
    return value.toLocaleString("en-US", {
        minimumFractionDigits: digits,
        maximumFractionDigits: digits
    });
}

// Like formatNumber, but adds a "+" in front of positive numbers.
function formatSigned(value) {
    if (value >= 0) {
        return "+" + formatNumber(value);
    }
    return formatNumber(value);
}

// Bootstrap text color class: green for gains, red for losses.
function textColorClass(value) {
    if (value >= 0) {
        return "text-success";
    }
    return "text-danger";
}

// Bootstrap badge class: green background for gains, red for losses.
function badgeColorClass(value) {
    if (value >= 0) {
        return "text-bg-success";
    }
    return "text-bg-danger";
}

// Chart.js draws on a <canvas>, which can't use CSS variables directly.
// So we ask the browser for the actual color of a Bootstrap variable,
// e.g. getThemeColor("--bs-success") gives something like "#198754".
function getThemeColor(variableName) {
    const styles = getComputedStyle(document.documentElement);
    return styles.getPropertyValue(variableName).trim();
}

// Switch Bootstrap between its light and dark themes.
function applyTheme(isDark) {
    if (isDark) {
        document.documentElement.setAttribute("data-bs-theme", "dark");
    } else {
        document.documentElement.setAttribute("data-bs-theme", "light");
    }
}


/* ==========================================================================
   SECTION 4: FAKE PRICE HISTORY
   We don't have real historical prices, so we invent a believable line.
   It is "deterministic": the same stock always gets the same line, and
   the last point always equals the current price.
   ========================================================================== */

function makePriceHistory(holding, days) {
    // Turn the stock symbol into a starting number ("seed") for the random generator.
    let seed = 0;
    for (let i = 0; i < holding.symbol.length; i++) {
        seed = seed * 31 + holding.symbol.charCodeAt(i);
    }

    // Returns a pseudo-random number between 0 and 1.
    function nextRandom() {
        seed = (seed * 16807) % 2147483647;
        return seed / 2147483647;
    }

    // Stocks that are up overall drift upward over time, and vice versa.
    const drift = (holding.price / holding.cost - 1) * 0.35;

    // Start at 1, then wobble a little each day.
    const prices = [1];
    for (let day = 1; day < days; day++) {
        const yesterday = prices[day - 1];
        const randomWobble = (nextRandom() - 0.5) * 0.03;
        const dailyMove = randomWobble + drift / days;
        prices.push(yesterday * (1 + dailyMove));
    }

    // Scale every point so the final one matches the real current price.
    const scale = holding.price / prices[days - 1];
    for (let i = 0; i < prices.length; i++) {
        prices[i] = prices[i] * scale;
    }

    return prices;
}


/* ==========================================================================
   SECTION 5: CALCULATIONS
   Turn each holding into a "row" with all the numbers the table needs.
   ========================================================================== */

function calculateRows() {
    const rows = [];

    for (let i = 0; i < holdings.length; i++) {
        const h = holdings[i];

        const row = {
            symbol: h.symbol,
            name: h.name,
            sector: h.sector,
            price: h.price,
            quantity: h.quantity,
            cost: h.cost,

            // Today's change per share, and as a percent.
            change: h.price - h.previousPrice,
            changePercent: (h.price / h.previousPrice - 1) * 100,

            // What the whole position is worth, and what it cost.
            marketValue: h.price * h.quantity,
            costBasis: h.cost * h.quantity,

            // Gain today, and gain since you bought.
            dayGain: (h.price - h.previousPrice) * h.quantity,
            totalGain: (h.price - h.cost) * h.quantity,
            totalGainPercent: (h.price / h.cost - 1) * 100
        };

        // Fake price line for the sparkline and the portfolio trend.
        row.history = makePriceHistory(row, selectedDays);
        row.trendIsUp = row.history[row.history.length - 1] >= row.history[0];

        rows.push(row);
    }

    return rows;
}

// Sort the rows by the current column and direction.
function sortRows(rows) {
    rows.sort(function (a, b) {
        let result;

        if (typeof a[sortColumn] === "string") {
            result = a[sortColumn].localeCompare(b[sortColumn]); // text: A to Z
        } else {
            result = a[sortColumn] - b[sortColumn];              // numbers: small to large
        }

        return result * sortDirection; // multiplying by -1 flips the order
    });
}

// Add up the numbers for the whole portfolio.
function calculateTotals(rows) {
    const totals = { marketValue: 0, dayGain: 0, totalGain: 0, costBasis: 0 };

    for (let i = 0; i < rows.length; i++) {
        totals.marketValue += rows[i].marketValue;
        totals.dayGain += rows[i].dayGain;
        totals.totalGain += rows[i].totalGain;
        totals.costBasis += rows[i].costBasis;
    }

    // Percentages are measured against what the portfolio was worth before.
    const valueYesterday = totals.marketValue - totals.dayGain;
    totals.dayGainPercent = totals.dayGain / valueYesterday * 100;
    totals.totalGainPercent = totals.totalGain / totals.costBasis * 100;

    return totals;
}


/* ==========================================================================
   SECTION 6: DRAWING
   Each function below draws one part of the page.
   ========================================================================== */

// The clickable column titles. The sorted column gets an aria-sort
// attribute (used by screen readers and by CSS for the little arrow).
function drawTableHeader() {
    let html = "";

    for (let i = 0; i < columns.length; i++) {
        const column = columns[i];

        let sortAttribute = "";
        if (column.key === sortColumn) {
            if (sortDirection === 1) {
                sortAttribute = 'aria-sort="ascending"';
            } else {
                sortAttribute = 'aria-sort="descending"';
            }
        }

        html += `<th data-key="${column.key}" ${sortAttribute} tabindex="0" scope="col">${column.label}</th>`;
    }

    document.getElementById("head").innerHTML = html;
}

// One <tr> per holding, with a small canvas in the last cell for the sparkline.
function drawTableBody(rows) {
    let html = "";

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];

        html += `
            <tr>
                <td>
                    <span class="fw-bold text-primary">${row.symbol}</span>
                    <span class="d-block small text-body-secondary text-truncate" style="max-width:190px">${row.name}</span>
                </td>
                <td>${formatNumber(row.price)}</td>
                <td class="${textColorClass(row.change)}">${formatSigned(row.change)}</td>
                <td><span class="badge ${badgeColorClass(row.changePercent)}">${formatSigned(row.changePercent)}%</span></td>
                <td>${formatNumber(row.quantity, 0)}</td>
                <td>${formatNumber(row.cost)}</td>
                <td><b>${formatDollars(row.marketValue)}</b></td>
                <td class="${textColorClass(row.dayGain)}">${formatSigned(row.dayGain)}</td>
                <td class="${textColorClass(row.totalGain)}">${formatSigned(row.totalGain)}</td>
                <td class="${textColorClass(row.totalGainPercent)}">${formatSigned(row.totalGainPercent)}%</td>
                <td>
                    <div style="width:90px;height:28px;margin-left:auto">
                        <canvas id="spark-${row.symbol}" aria-hidden="true"></canvas>
                    </div>
                </td>
            </tr>`;
    }

    document.getElementById("body").innerHTML = html;
}

// The "Total" row at the bottom of the table.
function drawTableFooter(totals) {
    document.getElementById("foot").innerHTML = `
        <tr>
            <td>Total</td>
            <td></td><td></td><td></td><td></td><td></td>
            <td>${formatDollars(totals.marketValue)}</td>
            <td class="${textColorClass(totals.dayGain)}">${formatSigned(totals.dayGain)}</td>
            <td class="${textColorClass(totals.totalGain)}">${formatSigned(totals.totalGain)}</td>
            <td class="${textColorClass(totals.totalGain)}">${formatSigned(totals.totalGainPercent)}%</td>
            <td></td>
        </tr>`;
}

// The big numbers at the top of the summary card.
function drawSummary(rows, totals) {
    document.getElementById("mv").textContent = formatDollars(totals.marketValue);

    document.getElementById("dayline").innerHTML = `
        <span class="${textColorClass(totals.dayGain)}">
            <b>${formatSigned(totals.dayGain)}</b> (${formatSigned(totals.dayGainPercent)}%)
        </span>
        <span class="small text-body-secondary">today</span>`;

    document.getElementById("tg").innerHTML = `
        <span class="${textColorClass(totals.totalGain)}">
            ${formatSigned(totals.totalGain)} (${formatSigned(totals.totalGainPercent)}%)
        </span>`;

    document.getElementById("cb").textContent = formatDollars(totals.costBasis);
    document.getElementById("cnt").textContent = rows.length + " positions";
}

// The line chart of total portfolio value over the selected range.
function drawTrendChart(rows) {
    // For each day, add up (price that day x quantity) across all holdings.
    const portfolioValues = [];
    for (let day = 0; day < selectedDays; day++) {
        let valueThatDay = 0;
        for (let i = 0; i < rows.length; i++) {
            valueThatDay += rows[i].history[day] * rows[i].quantity;
        }
        portfolioValues.push(valueThatDay);
    }

    // Label each day with a date, counting back from today.
    const labels = [];
    const oneDayInMilliseconds = 24 * 60 * 60 * 1000;
    for (let day = 0; day < selectedDays; day++) {
        const daysAgo = selectedDays - 1 - day;
        const date = new Date(Date.now() - daysAgo * oneDayInMilliseconds);
        labels.push(date.toLocaleDateString("en-US", { month: "short", day: "numeric" }));
    }

    // Green if the portfolio ended higher than it started, otherwise red.
    const endedHigher = portfolioValues[portfolioValues.length - 1] >= portfolioValues[0];
    const lineColor = getThemeColor(endedHigher ? "--bs-success" : "--bs-danger");
    const textColor = getThemeColor("--bs-secondary-color");
    const gridColor = getThemeColor("--bs-border-color");

    // Remove the previous chart before drawing a new one.
    if (trendChart) {
        trendChart.destroy();
    }

    trendChart = new Chart(document.getElementById("main"), {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                data: portfolioValues,
                borderColor: lineColor,
                backgroundColor: lineColor + "1f", // "1f" makes the fill see-through
                fill: true,
                borderWidth: 2,
                pointRadius: 0,        // hide dots...
                pointHoverRadius: 4,   // ...until you hover
                tension: 0.25          // slightly curved line
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: { mode: "index", intersect: false },
            plugins: {
                legend: { display: false },
                tooltip: {
                    displayColors: false,
                    callbacks: {
                        label: function (context) {
                            return formatDollars(context.parsed.y);
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: { display: false },
                    ticks: { color: textColor, maxTicksLimit: 6, maxRotation: 0 }
                },
                y: {
                    grid: { color: gridColor },
                    ticks: {
                        color: textColor,
                        maxTicksLimit: 4,
                        callback: function (value) {
                            return "$" + (value / 1000).toFixed(1) + "k";
                        }
                    }
                }
            }
        }
    });
}

// The doughnut chart and its legend: how much of the portfolio is in each sector.
function drawDonutChart(rows, totals) {
    // Add up the market value of each sector, e.g. { Technology: 30000, Energy: 5000 }
    const valueBySector = {};
    for (let i = 0; i < rows.length; i++) {
        const sector = rows[i].sector;
        if (valueBySector[sector] === undefined) {
            valueBySector[sector] = 0;
        }
        valueBySector[sector] += rows[i].marketValue;
    }

    // Turn that object into a list and put the biggest sector first.
    const sectors = [];
    for (const sectorName in valueBySector) {
        sectors.push({ name: sectorName, value: valueBySector[sectorName] });
    }
    sectors.sort(function (a, b) {
        return b.value - a.value;
    });

    // Separate lists for the chart's labels, values, and colors.
    const names = [];
    const values = [];
    const colors = [];
    for (let i = 0; i < sectors.length; i++) {
        names.push(sectors[i].name);
        values.push(sectors[i].value);
        colors.push(sectorColors[i % sectorColors.length]); // % wraps around if we run out
    }

    if (donutChart) {
        donutChart.destroy();
    }

    donutChart = new Chart(document.getElementById("donut"), {
        type: "doughnut",
        data: {
            labels: names,
            datasets: [{
                data: values,
                backgroundColor: colors,
                borderColor: getThemeColor("--bs-body-bg"),
                borderWidth: 2
            }]
        },
        options: {
            cutout: "62%", // size of the hole
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }, // we draw our own legend below
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return " " + context.label + ": " + formatDollars(context.parsed, 0);
                        }
                    }
                }
            }
        }
    });

    // Our own legend: a colored dot, the sector name, and its percentage.
    let legendHtml = "";
    for (let i = 0; i < sectors.length; i++) {
        const percent = sectors[i].value / totals.marketValue * 100;
        legendHtml += `
            <div>
                <span>
                    <span class="dot" style="background:${colors[i]}"></span>${sectors[i].name}
                </span>
                <b>${formatNumber(percent, 1)}%</b>
            </div>`;
    }
    document.getElementById("legend").innerHTML = legendHtml;
}

// The tiny line chart in the last cell of each table row.
// This must run AFTER drawTableBody, because it needs those canvases to exist.
function drawSparklines(rows) {
    // Delete the old sparkline charts first.
    for (let i = 0; i < sparklineCharts.length; i++) {
        sparklineCharts[i].destroy();
    }
    sparklineCharts = [];

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        const canvas = document.getElementById("spark-" + row.symbol);

        // Number the days 0, 1, 2... (labels are required, but we hide them).
        const dayNumbers = [];
        for (let day = 0; day < row.history.length; day++) {
            dayNumbers.push(day);
        }

        const color = getThemeColor(row.trendIsUp ? "--bs-success" : "--bs-danger");

        const chart = new Chart(canvas, {
            type: "line",
            data: {
                labels: dayNumbers,
                datasets: [{
                    data: row.history,
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
                events: [], // no hover effects on these tiny charts
                plugins: { legend: { display: false }, tooltip: { enabled: false } },
                scales: { x: { display: false }, y: { display: false } }
            }
        });

        sparklineCharts.push(chart);
    }
}

// Redraw everything. We call this on page load and after every click.
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

    document.getElementById("head").addEventListener("click", function (event) {
        const th = event.target.closest("th");

        if (!th) {
            return;
        }

        const clickedColumn = th.dataset.key; // reads the data-key attribute
        if (clickedColumn === "trend") {
            return; // the Trend column can't be sorted
        }

        if (clickedColumn === sortColumn) {
            sortDirection = -sortDirection;  // same column again: flip the order
        } else {
            sortColumn = clickedColumn;      // new column: start fresh...
            if (clickedColumn === "symbol") {
                sortDirection = 1;           // ...A to Z for the symbol
            } else {
                sortDirection = -1;          // ...biggest first for numbers
            }
        }

        drawAll();
    });

    // Pressing Enter on a focused column title acts like a click (keyboard users).
    document.getElementById("head").addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            event.target.click();
        }
    });

    // Clicking 1W / 1M / 3M changes the chart range.
    document.getElementById("range").addEventListener("click", function (event) {
        const clickedButton = event.target.closest("button");
        if (!clickedButton) {
            return;
        }

        selectedDays = Number(clickedButton.dataset.r); // the button's data-r attribute

        // Highlight only the button that was clicked.
        const buttons = document.querySelectorAll("#range button");
        for (let i = 0; i < buttons.length; i++) {
            const isClicked = buttons[i] === clickedButton;
            buttons[i].setAttribute("aria-pressed", isClicked);
            buttons[i].classList.toggle("active", isClicked);
        }

        drawAll();
    });

    drawAll();
}

main();
