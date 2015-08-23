/// <reference path="../jquery-1.10.2.min.js" />
(function () {
    $('#Plenka').hide();
    $('#ConteinerPrintDog').hide();
    var $PrintButton = $('#PrintButton'),
        $ConteinerPrintDog = $('#ConteinerPrintDog'),
        $Plenka = $('#Plenka')

    $CreateButton.click(function () {
        $.ajax({
            type: "GET",
            url: Links.GetAllDogs,
            cashe: false,
            success: function (data) {
                $MessageText.empty();
                if (data.success) {
                    $MessageText.append("Пользователь с id = " + data.Name + " успешно добавлен");
                }
                else {
                    $MessageText.append(data.ErrorMessage);
                }
                loadAllUser();
            }
        })
    })

    loadAllUser();
})()

//function loadAllUser() {
//    $UserList.empty();
//    $.ajax({
//        type: "GET",
//        url: Links.GetAllUsers,
//        cashe: false,
//        success: function (data) {
//            for (i in data) {
//                $UserList.append("<li>" + data[i].Name + "</li>");
//            }
//        }
//    })
//}

