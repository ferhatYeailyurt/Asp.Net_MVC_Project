$(function () {
    $("#tblTableView").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.12/i18n/Turkish.json"
        }
        });
    $("#tblTableView").on("click", ".btnDepartmanSil", function () {
        var btn = $(this);
        bootbox.confirm("Departmanı silmek istediğinizden emin misiniz?", function (result) {
            if (result) {

                var id = btn.data("id");
                
                $.ajax({
                    type: "Get",
                    url: "/Departman/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();

                    }


                });
            }

          

        })
       
        
    });

});
function CheckDateTypeIsValid(dataElement)
{
    var value = $(dataElement).val();
    if (value = '') {
        $this(dataElement).valid("false");

    }
    else {
        $(dataElement).valid();
    }
}
