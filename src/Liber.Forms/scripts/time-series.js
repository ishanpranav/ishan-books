// time-series.js
// Copyright(c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const report = window.chrome.webview.hostObjects.report;
const chart = document.getElementById('chart');

Chart.defaults.font.family = 'Segoe UI';
Chart.defaults.font.size = 11;
Chart.defaults.font.weight = 'normal';

const config = {
    type: 'line',
    options: {
        responsive: true,
        maintainAspectRatio: false,
        interaction: { mode: 'index', intersect: false },
        plugins: {
            title: {
                display: true,
                font: {
                    size: 20,
                    weight: 'bold'
                }
            },
            legend: { display: false }
        },
        scales: {
            x: {
                grid: { display: false },
                ticks: { color: '#212529', maxTicksLimit: 6, maxRotation: 0 }
            },
            y: {
                grid: { color: '#dee2e6' },
                ticks: {
                    color: '#212529',
                    maxTicksLimit: 4
                }
            }
        }
    }
};

async function main() {
    config.options.plugins.title.text = await report.title;
    config.data = JSON.parse(await report.getTimeSeries);
}

main().then(() => {
    new Chart(chart, config);
});
