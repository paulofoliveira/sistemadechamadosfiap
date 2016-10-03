function onSuccessInteracao(response) {
    $("#interacao-list").prepend(response);
    $("#interacao-modal").modal("hide");
}

function onFailureInteracao(response) {

}

$(document).ready(function () {

    $(document).on("click", ".interacao-remove", function (e) {

        var $interacaoDetail = $(this).closest(".interacao-detail"),
            id = $interacaoDetail.attr("data-id");

        if (confirm("Deseja remover a interação?")) {

            if (id > 0) {
                $.ajax({
                    url: "/Chamado/ExcluirInteracao/" + $interacaoDetail.attr("data-id"),
                    type: "GET",
                    success: function (response) {
                        $interacaoDetail.remove();
                    }
                });
            }
            else
            {
                $interacaoDetail.remove();
            }
        }

        e.preventDefault();
    });

});