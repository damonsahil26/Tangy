window.showToastr = (type, message) => {
    if (type === 'success') {
        toastr.success(message, 'Operation Successful');
    }
 if (type === 'error') {
        toastr.error(message, 'Operation Failed');
    }
}

window.showSweetAlert = (type, message) => {
    Swal.fire({
        title: type,
        text: message,
        icon: type
    });
}

function showDeleteConfirmationPopUp() {
    $('#deleteConfimationPopUp').modal('show')
}

function hideDeleteConfirmationPopUp() {
    $('#deleteConfimationPopUp').modal('hide')
}
