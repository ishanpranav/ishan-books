// sankey-diagram.js
// Copyright(c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const report = window.chrome.webview.hostObjects.report;
const chart = document.getElementById('chart');

Chart.defaults.font.family = 'Segoe UI';
Chart.defaults.font.size = 11;
Chart.defaults.font.weight = 'normal';

const config = {
    type: 'sankey',
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: false
            },
            title: {
                display: true,
                font: {
                    size: 20,
                    weight: 'bold'
                }
            },
            tooltips: {
                callbacks: {
                    title: () => ""
                }
            }
        }
    }
};

async function main() {
    config.options.plugins.title.text = await report.title;

    // const colors = {
    //     a: 'red',
    //     b: 'green',
    //     c: 'blue',
    //     d: 'gray'
    // };

    const getColor = (key) => colors[key];

    config.data = {
        datasets: [{
            data: JSON.parse(await report.getSankeyDiagram),
            colorFrom: (ctx) => ctx.raw.fromColor,
            colorTo: (ctx) => ctx.raw.toColor,
            colorMode: 'gradient',
            size: 'max'
        }]
    };
}

main().then(() => {
    new Chart(chart, config);
});
