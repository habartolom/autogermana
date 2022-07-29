var dataTable;

$(document).ready(() => {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticulos").dataTable({
        "ajax": {
            "url": "Productos/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idproducto", "width": "5%" },
            { "data": "idcategoria", "width": "5%" },
            { "data": "codigo", "width": "5%" },
            { "data": "nombre", "width": "10%" },
            { "data": "precioVenta", "width": "10%" },
            { "data": "stock", "width": "5%" },
            { "data": "descripcion", "width": "10%" },
            { "data": "imagen", "width": "5%" },
            { "data": "estado", "width": "10%" },
            {
                "data": "idproducto",
                "render": (data) => `<div class = "text-center">
                                        <a href="/Productos/Edit/${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                                            <i class="fas fa-edit"></i> Editar
                                        </a>
                                        &nbsp;
                                        <a onclick=Delete("/Productos/Delete/${data}") class="btn btn-danger text-white" style="cursor: pointer; width: 100px;">
                                            <i class="fas fa-trash-alt"></i> Borrar
                                        </a>
                                    </div>`,
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, async function () {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const data = await response.json();
        if (data.success) {
            toastr.success(data.message);
            dataTable.api().ajax.reload()
        }
        else
            toastr.error(data.message)
    });


}