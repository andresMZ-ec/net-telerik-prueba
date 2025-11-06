let uploadsID = []

window.addEventListener("beforeunload", function () {

    uploadsID.map((id) => {
        const data = new FormData();
        data.append("Guid", id);

        navigator.sendBeacon("/Content/CleanTempHandler.ashx", data);
    });
});

function InicializarEventosUploadComponente(btnSelectedId, fileUploadId, btnProcessId) {
    function init() {
        const btnSelected = document.getElementById(btnSelectedId);
        const FileUpload = document.getElementById(fileUploadId);
        const ButtonProcess = document.getElementById(btnProcessId);

        if (!btnSelected || !FileUpload) {
            setTimeout(init, 50);
            return;
        }

        btnSelected.addEventListener("click", (e) => {
            e.preventDefault();
            FileUpload.click();
        });

        FileUpload.addEventListener("change", () => {
            
            const files = FileUpload.files;
            if (files.length === 0) return;

            const formData = new FormData();
            for (let i = 0; i < files.length; i++) {
                formData.append("files", files[i]);
            }

            formData.append("uploadID", fileUploadId)
            uploadsID.push(fileUploadId)

            fetch("UploadHandler.ashx", {
                method: "POST",
                body: formData
            })
                .then(resp => resp.text())
                .then(__doPostBack(btnProcessId, ''))
                .catch(err => console.error(err));
        });
    }

    init();
}
