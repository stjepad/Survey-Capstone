console.log("bloop")

let questions = document.querySelectorAll(".question-data")

questions.forEach(q => {
    let label = q.defaultValue;
    let num = q.id.split("-")[1]

    let answerInputs = document.querySelectorAll(".answer-data")
    let answerArray = [];
    answerInputs.forEach(a => answerArray.push(a));

    let answers = answerArray.filter(a => a.id.split("-")[3] === num)

    let data = [];
    let labels = [];

    answers.forEach(a => {
        let text = a.defaultValue;
        labels.push(text.split("--")[0])
        data.push(parseInt(text.split("--")[1]))
    })


    var ctx = document.getElementById(`myChart-${num}`).getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.5)',
                    'rgba(54, 162, 235, 0.5)',
                    'rgba(255, 206, 86, 0.5)',
                    'rgba(75, 192, 192, 0.5)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
            }
        }
    });
})

