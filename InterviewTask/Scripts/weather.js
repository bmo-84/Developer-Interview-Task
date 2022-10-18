$('.weather-info').each(function () {
    var weatherLocation = $(this).attr('data-location');
    var weatherSpan = $(this);
    $.ajax({
        url: '/weather?city=' + weatherLocation,
        success: function (result) {
            var span = document.createElement("span");
            span.innerHTML = '<img src="' + result.IconPath + '" alt="' + result.Summary + '" /> ' + result.Temperature + '&deg;C';
            $(weatherSpan).html(span);
        }
    });
});