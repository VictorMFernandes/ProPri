function updateImage(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            $(input).siblings("img")
                .attr("src", e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function clearImage() {
    $(".image-input")
        .attr("src", ConstPersonDefaultImage);

    $("#id-img-upload").val(null);
    $("#id-delete-original-image").val("true");
}