function ImageToBase64(inputFile, dataCallback) {
    if (typeof (FileReader) === 'undefined') {
        alert("抱歉，你的浏览器不支持 FileReader，不能将图片转换为Base64！");
    } else {
        try {
            /*图片转Base64 核心代码*/
            var file = inputFile.files[0];
            if (!/image\/\w+/.test(file.type)) {
                alert("请确保文件为图像类型");
                return false;
            }
            var reader = new FileReader();
            reader.onload = function () {
                dataCallback(this.result);
            }
            reader.readAsDataURL(file);
        } catch (e) {
            alert('图片转Base64出错' + e.toString())
        }
    }
}

function ShowMovie(movieId) {
    layer.open({
        type: 2,
        title: "影片详情",
        shadeClose: false,
        shade: 0.4,
        area: ['900px', '90%'],
        content: '/Movie/Detail/' + movieId,
    });
}