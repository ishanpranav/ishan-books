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
            legend: { display: false },
            tooltip: {
                callbacks: {
                    title: (items) => {
                        return new Date(items[0].parsed.x).toLocaleDateString(undefined, { dateStyle: 'medium' });
                    }
                }
            }
        },
        scales: {
            x: {
                type: 'time',
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

function toTimestamp(date) {
    if (typeof date === 'string' && date.length === 10) {
        return new Date(date + 'T00:00:00').getTime();
    }

    return new Date(date).getTime();
}

function toTimePoints(data) {
    const dates = data.labels || [];
    const datasets = data.datasets || [];

    for (let i = 0; i < datasets.length; i++) {
        const points = [];

        for (let j = 0; j < datasets[i].data.length; j++) {
            points.push({ x: toTimestamp(dates[j]), y: datasets[i].data[j] });
        }

        datasets[i].data = points;
    }

    delete data.labels;

    return data;
}

async function main() {
    config.options.plugins.title.text = await report.title;
    config.data = toTimePoints(JSON.parse(await report.getTimeSeries));
}

main().then(() => {
    new Chart(chart, config);
});
