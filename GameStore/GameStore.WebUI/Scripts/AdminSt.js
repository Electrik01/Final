google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);
var DateStr = @Html.Raw(Json.Encode(@ViewBag.Date));
var CountStr = @Html.Raw(Json.Encode(@ViewBag.Count));
let subarray = []; //массив в который будет выведен результат.
let mas = ["Дата", "Покупок"];
subarray.push(mas);
for (let i = 11; i >= 0; i--) {
    let mas_f = [DateStr[i], CountStr[i]];
    subarray.push(mas_f);
}


console.log(subarray);
function drawChart() {
    var data = google.visualization.arrayToDataTable(
        subarray
    );
    var options = {
        title: 'Количество продаж',
        vAxis: { minValue: 0 }
    };

    var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}


//Способ оплаты
        google.charts.load("current", {packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
         var OplataStr  = @Html.Raw(Json.Encode(@ViewBag.CountOplata));
        let oplata = []; //массив в который будет выведен результат.
        mas = ["Способ", "Количество"];
        oplata.push(mas);
        mas = ["Банковская карта", OplataStr[0]];
        oplata.push(mas);
        mas = ["QIWI", OplataStr[1]];
        oplata.push(mas);
        mas = ["WebMoney", OplataStr[2]];
        oplata.push(mas);
    function drawChart() {
        var data = google.visualization.arrayToDataTable(
            oplata
        );

        var options = {
            title: 'Способ оплаты',
        pieHole: 0,
    };

    var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
    chart.draw(data, options);
}
console.log(oplata);

    //По жанрам
            google.charts.load("current", {packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            var Genres  = @Html.Raw(Json.Encode(@ViewBag.Genres));
            var CountGenr  = @Html.Raw(Json.Encode(@ViewBag.CountGenr));
            let genres_list = []; //массив в который будет выведен результат.
             mas = ["Жанр", "Количество"];
            genres_list.push(mas);
    for (let i = 0; i < 5; i++) {
                let mas_f = [Genres[i], CountGenr[i]];
            genres_list.push(mas_f);
        }
    function drawChart() {
        var data = google.visualization.arrayToDataTable(
                genres_list
            );
    
        var options = {
                title: 'За жанром',
            pieHole: 0,
        };

        var chart = new google.visualization.PieChart(document.getElementById('donutchart2'));
        chart.draw(data, options);
    }
    console.log(oplata);
