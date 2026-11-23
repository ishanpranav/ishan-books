// pie-chart-normalize.js
// Copyright(c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

function normalizeChartData(chartData, threshold = 0.02) {
    const dataset = chartData.datasets[0];
    const total = dataset.data.reduce((sum, value) => sum + value, 0);
    const sortedData = chartData.labels.map((label, index) => ({
        label: label,
        value: dataset.data[index],
        color: Array.isArray(dataset.backgroundColor)
            ? dataset.backgroundColor[index]
            : undefined
    })).sort((a, b) => b.value - a.value);
    const topItems = sortedData.filter(item => item.value / total >= threshold);
    const remainingItems = sortedData.filter(item => item.value / total < threshold);
    const otherTotal = remainingItems.reduce(
        (sum, item) => sum + item.value, 0
    );
    const result = {
        labels: topItems.map(x => x.label),
        datasets: [{
            ...dataset,
            data: topItems.map(x => x.value),
            backgroundColor: topItems.map(x => x.color ?? "#f5f5f5")
        }]
    };

    if (otherTotal > 0) {
        result.labels.push("Other");
        result.datasets[0].data.push(otherTotal);
        result.datasets[0].backgroundColor.push("#f5f5f5");
    }

    return result;
}
