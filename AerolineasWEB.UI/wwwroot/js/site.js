// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", (event) => {
   
    document.querySelectorAll('#frm_del_aerolinea').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); //detiene el POST automático

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Esta aerolínea será desactivada',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar',
                confirmButtonColor: '#28a745'
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit(); //pots del form
                }
            });
        });
    });
    document.querySelectorAll('#frm_del_avion').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); //detiene el POST automático

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Este avión será desactivado',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar',
                confirmButtonColor: '#28a745'
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit(); //pots del form
                }
            });
        });
    });
    document.querySelectorAll('#frm_del_propietario').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); //detiene el POST automático

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Este propietario será desactivado',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar',
                confirmButtonColor: '#28a745'
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit(); //pots del form
                }
            });
        });
    });

    //Validacion si existen
    if (document.querySelector('#propietarioSelect') && document.querySelector('#aerolineaSelect')) {
        new TomSelect("#propietarioSelect", {
            maxItems: 1,
            valueField: 'value',
            labelField: 'text',
            searchField: ['text', 'tipo', 'pais'],

            render: {
                option: function (data, escape) {
                    return `
                <div>
                    <div style="font-weight: bold;">
                        ${escape(data.text)}
                    </div>
                    <div style="font-size: 12px; color: gray;">
                        ${escape(data.tipo || '')} - ${escape(data.pais || '')}
                    </div>
                </div>
            `;
                }
            }
        });

        new TomSelect("#aerolineaSelect", {
            maxItems: 1,
            valueField: 'value',
            labelField: 'text',
            searchField: ['text', 'pais'],

            render: {
                option: function (data, escape) {
                    return `
                <div>
                    <div style="font-weight: bold;">
                        ${escape(data.text)}
                    </div>
                    <div style="font-size: 12px; color: gray;">
                        ${escape(data.pais || '')}
                    </div>
                </div>
            `;
                }
            }
        });
    }

    if (document.querySelector('#formulario_aerolinea') || document.querySelector('#formulario_propietario')) {
        const formAero = document.querySelector('#formulario_aerolinea');
        const formProp = document.querySelector('#formulario_propietario');
        const form = formAero || formProp
        if (form) {
            form.addEventListener("submit", function (e) {

                const telefono = document.querySelector('#telefono');
                const error = document.querySelector('#telefono-error');

                let limpio = telefono.value.replace(/\D/g, '');

                if (limpio.length < 11) {
                    e.preventDefault();

                    error.textContent = "Debe ingresar un número de teléfono válido";
                   /* telefono.classList.add("is-invalid");*/
                } else {
                    error.textContent = "";
                }
            });
        }
    }
    if (document.querySelector(".table")) {
        const $tabla = $('.table');
        $.fn.dataTable.ext.errMode = 'none';
        if (!$.fn.DataTable.isDataTable($tabla)) {

            $tabla.DataTable({
                paging: true,
                pageLength: 10,
                order: [[0, 'desc']],
                autoWidth: false,
                ordering: false, 
                dom: 'rtip',
                pagingType: "simple_numbers",
                dom: 'rt<"d-flex justify-content-between mt-3"ip>',
                language: {
                    url: "https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json",
                    paginate: {
                        previous: '<i class="fa-solid fa-chevron-left"></i>',
                        next: '<i class="fa-solid fa-chevron-right"></i>'
                    }
                }
            });

        }
        
    }
});