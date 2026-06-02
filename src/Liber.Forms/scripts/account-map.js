// account-map.js
// Copyright(c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const report = window.chrome.webview.hostObjects.report;
const chart = document.getElementById('chart');

Chart.defaults.font.family = 'Segoe UI';
Chart.defaults.font.size = 11;
Chart.defaults.font.weight = 'normal';

const config = {
    type: 'treemap',
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

    const tree = JSON.parse(await report.getAccountMap);
    const colorMap = Object.fromEntries(tree.map(d => [d.name, d.backgroundColor]));
    const labelMap = Object.fromEntries(tree.map(d => [d.name, d.displayName]));
    const key = 'Balance';

    for (const node of tree) {
        node[key] = node.value;
    }

    config.data = {
        datasets: [{
            borderColor: '#ffffff',
            borderWidth: 1,
            borderRadius: 4,
            spacing: 2,
            backgroundColor: (ctx) => {
                const label = ctx.raw?.g ?? ctx.raw?._data?.name;

                return colorMap[label] ?? '#f8f9fa';
            },
            tree: tree,
            key: key,
            groups: ['parent', 'name']
        }]
    };
}

main().then(() => {
    new Chart(chart, config);
});
