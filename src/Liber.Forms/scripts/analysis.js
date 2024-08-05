﻿// analysis.js
// Copyright(c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const report = window.chrome.webview.hostObjects.report;
const chart = document.getElementById('chart');

const config = {
    type: 'pie',
    options: {
        plugins: {
            title: {
                display: true
            }
        }
    }
};

async function main() {
    config.options.plugins.title.text = await report.title;
    config.data = JSON.parse(await report.getAnalysis);
}

main().then(() => {
    new Chart(chart, config);
});
