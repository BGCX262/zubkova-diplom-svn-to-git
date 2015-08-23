$('#datepicker').datepicker({
    dateFormat: 'dd.mm.yy'
});
$(function () {
    $("#datepicker").datepicker();
});
$('#datepicker').datepicker('option', $.datepicker.regional["ru"]);

jQuery(function ($) {
    $.datepicker.regional['ru'] = {
        monthNames: ['Яварь', 'Февраль', 'Март', 'Апрель',
        'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь',
        'Октябрь', 'Ноябрь', 'Декабрь'],
        dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        firstDay: 1,
    };
    $.datepicker.setDefaults($.datepicker.regional['ru']);
});
$.datepicker.setDefaults($.extend(
  $.datepicker.regional["ru"])
);
