window.showModal = (id) => {
    const modal = new bootstrap.Modal(document.getElementById(id));
    modal.show();
}

window.hideModal = (id) => {
    const el = document.getElementById(id)
    const modal = bootstrap.Modal.getInstance(el)
    modal.hide()
}