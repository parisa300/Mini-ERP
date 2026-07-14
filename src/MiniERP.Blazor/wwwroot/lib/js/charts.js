
console.log("charts.js loaded");
window.renderSalesChart = (canvasId, labels, values) => {

     console.log("renderSalesChart()");
    console.log(labels);
    console.log(values);

    const canvas = document.getElementById(canvasId);

    if (!canvas)
        return;

    const oldChart = Chart.getChart(canvas);

    if (oldChart)
        oldChart.destroy();

    new Chart(canvas, {
        type: 'line',

        data: {
            labels: labels,

            datasets: [{
                label: 'Monthly Sales',
                data: values,
                borderColor: '#6C63FF',
                backgroundColor: 'rgba(108,99,255,.15)',
                fill: true,
                tension: 0.4
            }]
        },

        options: {

            responsive: true,
            maintainAspectRatio: false,

            plugins: {
                legend: {
                    display: false
                }
            }
            
        }
    });

};