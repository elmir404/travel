$(document).ready(function () {
    var pathname = window.location.search.substring(1);

    const linkValues = pathname.split("=");

    $("#updatePortfolio").click(function () {

        var files = $("#files").get(0).files;
        var formData = new FormData();
        formData.append('AzName', $("#azName").val());
        formData.append('EnName', $("#enName").val());
        formData.append('RuName', $("#ruName").val());
        formData.append('AzDescription', $("#azDescription").val());
        formData.append('EnDescription', $("#enDescription").val());
        formData.append('RuDescription', $("#ruDescription").val());
        formData.append('Price', $("#price").val());
        formData.append('CategoryId', $("#categoryId").val());


        for (var i = 0; i < files.length; i++) {
            formData.append('Files', files[i]);
        }


        $.ajax({
            type: "POST",
            url: `/AdminPanel/Product/Update?id=${linkValues[1]}`,
            data: formData,
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            complete: function (response) {
                if (response.responseJSON.success == true) {
                    location.href = "/AdminPanel/Product/List"
                    debugger;
                }

                else {
                    alert("error")
                    debugger;
                }
                debugger;

            },
        });


    });
});