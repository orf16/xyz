//Funcion para validar los campos generales de las metodologias GTC45,INSHT Y RAM
var esObligatorioFuente = true;
var esObligatorioMedio = true;
var esObligatorioTrabajador = true;
var urlPerfil = '/PerfilSocioDemoGrafico';
var validado = true;
var urlPerfil = '/PerfilSocioDemoGrafico';
var contCondicionesRiesgo = 1;

var IDOTRO = 8;// Constante que debe ser igual al id de la tabla(Sql Server) tipo de peligro para el peligro de tipo otro

function validarCamposInformacioGeneral(numberTab, metodologiaGTC) {

    if (metodologiaGTC == true) {
        esObligatorioFuente = false;
        esObligatorioMedio = false;
        esObligatorioTrabajador = false;
    } else {
        esObligatorioFuente = true;
        esObligatorioMedio = true;
        esObligatorioTrabajador = true;
    }

    FormularioCampoGenerales();
    if ($("#metodologia").valid()) {
        AsignarAtributosPestañaSiguiente(numberTab);
        removerAtributosPestañaAnterior(numberTab - 1);
    }

}


function AnteriorPanel(numberTab) {
    AsignarAtributosPestañaSiguiente(numberTab);
    removerAtributosPestañaAnterior(numberTab + 1);
    $("#Numero_De_Expuestos").focus()
}


function AsignarAtributosPestañaSiguiente(numberTab) {
    var tabSiguiente = "#tab" + numberTab;
    $(tabSiguiente).attr("href", "#step" + numberTab);
    $(tabSiguiente).attr("data-toggle", "tab");
    $(tabSiguiente).click();
    $("#Numero_De_Expuestos").focus()
}

function removerAtributosPestañaAnterior(numberTab) {
    var tabSiguiente = "#tab" + numberTab;
    $(tabSiguiente).removeAttr("href");
    $(tabSiguiente).removeAttr("data-toggle");
}

function FormularioCampoGenerales() {

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");

    $("#metodologia").validate({
        // errorClass: "my-error-class",
        rules: {
            Nombre_Del_Profesional: {
                required: true,
                maxlength: 30,
                lettersonly: true
            },
            Numero_De_Documento: {
                required: true,
                maxlength: 15,
                min: 1
            },
            Numero_De_Licencia_SST: {
                required: true,
                maxlength: 50
            },
            Fecha_De_Evaluacion: {
                required: true
            }, FK_Proceso: {
                required: true
            },
            Lugar: {
                required: true,
                maxlength: 30
            },
            Actividad: {
                required: true,
                maxlength: 50
            },
            Tarea: {
                required: true,
                maxlength: 30
            },
            FLG_Rutinaria: {
                required: true
            },
            Fuente_Generadora_De_Peligro: {
                required: true,
                maxlength: 100
            },
            FK_Tipo_De_Peligro: {
                required: true
            },
            FK_Clasificacion_De_Peligro: {
                required: true
            },
            Otro: {
                required: true,
                maxlength: 30
            },
            FLG_Tipo_De_Calificacion: {
                required: true
            },
            Horas_De_Exposicion_Planta: {
                required: true,
                maxlength: 2,
                min: 0,
                max: 24
            },
            Planta_Directo: {
                required: true,
                min: 0,
            },
            Horas_De_Exposicion_Contratista: {
                required: true,
                maxlength: 2,
                min: 0,
                max: 24
            },
            Contratista: {
                required: true,
                min: 0,
            },
            Horas_De_Exposicion_Temporal: {
                required: true,
                maxlength: 2,
                min: 0,
                max: 24
            },
            Temporal: {
                required: true,
                min: 0,
            },
            Horas_De_Exposicion_Estudiante: {
                required: true,
                maxlength: 2,
                min: 0,
                max: 24
            },
            Estudiante_Pasante: {
                required: true,
                min: 0,
            },
            Horas_De_Exposicion_Visitante: {
                required: true,
                maxlength: 2,
                min: 0,
                max: 24
            },
            Visitante: {
                required: true,
                min: 0,
            },
            Total: {
                required: true
            },
            Efectos_Posibles: {
                required: true,
                maxlength: 500
            },
            Fuente: {
                required: esObligatorioFuente,
                maxlength: 500
            },
            Medio: {
                required: esObligatorioMedio,
                maxlength: 500
            },
            Accion_De_Prevencion: {
                required: esObligatorioTrabajador,
                maxlength: 500
            },
            FK_Probabilidad: {
                required: true
            },
            FK_Nivel_De_Deficiencia: {
                required: true
            },
            FK_Nivel_De_Exposicion: {
                required: true
            },
            FK_Consecuencia: {
                required: true
            },
            Numero_De_Expuestos: {
                required: true,
                min: 1,
                maxlength: 5
            },
            Peor_Consecuencia: {
                required: true,
                maxlength: 50
            },
            FLG_Requisito_Legal: {
                required: true
            },
            Accion_Requerida: {
                required: true,
                maxlength: 1000
            },
            Responsable: {
                required: true,
                maxlength: 100
            },
            Fecha_Finalizacion: {
                required: true
            },
            FK_ProbabilidadPersona: {
                required: true
            },
            FK_ProbabilidadClientes: {
                required: true
            },
            FK_ProbabilidadEconomica: {
                required: true
            },
            FK_ProbabilidadImagenE: {
                required: true
            },
            FK_ProbabilidadAmbiental: {
                required: true
            },
            consecuenciaPersona: {
                required: true
            },
            consecuenciasClientes: {
                required: true
            },
            consecuenciasEconomica: {
                required: true
            },
            consecuenciasImagenE: {
                required: true
            },
            consecuenciasAmbiental: {
                required: true
            },
            Procesos: {
                required: true
            },
            FK_Proceso: {
                required: true
            },
            Eliminacion: {
                maxlength: 2000
            },
            Sustitucion: {
                maxlength: 2000
            },
            Controles_De_Ingenieria: {
                maxlength: 2000
            },
            Controles_Administrativos: {
                maxlength: 2000
            },
            Elementos_De_Proteccion: {
                maxlength: 2000
            },
            Medidas_De_Control: {
                maxlength: 1000
            },
            Procedimientos_De_Trabajo: {
                maxlength: 1000
            },
            Informacion: {
                maxlength: 1000
            },
            Formacion: {
                maxlength: 1000
            },
            Consecuencias_Reales: {
                maxlength: 500
            },
            Consecuencias_Potenciales: {
                maxlength: 500
            },
            Riesgo_Controlado: {
                required: true
            }
        },
        messages: {
            Nombre_Del_Profesional: {
                required: "Nombre del profesional obligatorio",
                maxlength: "La longitud máxima del nombre del profesional es de 30 caracteres"
            },
            Numero_De_Documento: {
                required: "Número de documento obligatorio",
                maxlength: "La longitud máxima del número de documento es de 15 caracteres",
                min: "Solo se permite el ingreso de números"
            },
            Numero_De_Licencia_SST: {
                required: "Número de licencia obligatorio",
                maxlength: "La longitud máxima del número de licencia es de 50 caracteres"
            },
            Fecha_De_Evaluacion: {
                required: "Fecha de evaluación obligatoria"
            },
            FK_Proceso: {
                required: "Se debe seleccionar un proceso"
            },
            Lugar: {
                required: "Lugar obligatorio",
                maxlength: "La longitud máxima del lugar es de 30 caracteres"
            },
            Actividad: {
                required: "Actividad obligatoria",
                maxlength: "La longitud máxima de la actividad es de 50 caracteres"
            },
            Tarea: {
                required: "Tarea obligatoria",
                maxlength: "La longitud máxima de la tarea es de 30 caracteres"
            },
            FLG_Rutinaria: {
                required: "Se debe seleccionar si es rutinaria"
            },
            Fuente_Generadora_De_Peligro: {
                required: "La fuente generada es obligatoria",
                maxlength: "La longitud máxima de la fuente generadora es de 100 caracteres"
            },
            FK_Tipo_De_Peligro: {
                required: "Se debe seleccionar un tipo de peligro"
            },
            FK_Clasificacion_De_Peligro: {
                required: "Se debe seleccionar una clase de peligro"
            },
            Otro: {
                required: "Se debe ingresar cual es el peligro",
                maxlength: "La longitud máxima es de 30 caracteres"
            },
            FLG_Tipo_De_Calificacion: {
                required: "Se debe seleccionar un tipo de calificación"
            },
            Horas_De_Exposicion_Planta: {
                required: "Se debe ingresar un valor",
                maxlength: "La longitud máxima es de 2 caracteres",
                min: "El número mínimo es de 0 horas",
                max: "El número máximo es de 24 horas"
            },
            Planta_Directo: {
                required: "Se debe ingresar una cantidad",
                min: "La cantidad debe ser positiva",
            },
            Horas_De_Exposicion_Contratista: {
                required: "Se debe ingresar un valor",
                maxlength: "La longitud máxima es de 2 caracteres",
                min: "El número mínimo es de 0 horas",
                max: "El número máximo es de 24 horas"
            },
            Contratista: {
                required: "Debe ingresar una cantidad",
                min: "La cantidad debe ser positiva",
            },
            Horas_De_Exposicion_Temporal: {
                required: "Se debe ingresar un valor",
                maxlength: "La longitud máxima es de 2 caracteres",
                min: "El número mínimo es de 0 horas",
                max: "El número máximo es de 24 horas"
            },
            Temporal: {
                required: "Se debe ingresar una cantidad",
                min: "La cantidad debe ser positiva",
            },
            Horas_De_Exposicion_Estudiante: {
                required: "Se debe ingresar un valor",
                maxlength: "La longitud máxima es de 2 caracteres",
                min: "El número mínimo es de 0 horas",
                max: "El número máximo es de 24 horas"
            },
            Estudiante_Pasante: {
                required: "Se debe ingresar una cantidad",
                min: "La cantidad debe ser positiva",
            },
            Horas_De_Exposicion_Visitante: {
                required: "Se debe ingresar un valor",
                maxlength: "La longitud máxima es de 2 caracteres",
                min: "El número mínimo es de 0 horas",
                max: "El número máximo es de 24 horas"
            },
            Visitante: {
                required: "Se debe ingresar una cantidad",
                min: "La cantidad debe ser positiva",
            },
            Total: {
                required: "Se debe ingresar un valor"
            },
            Efectos_Posibles: {
                required: "Los efectos posibles son obligatorios",
                maxlength: "La longitud máxima de los efectos posibles es de 500 caracteres"
            },
            Fuente: {
                required: "La fuente es obligatoria",
                maxlength: "La longitud máxima de la fuente es de 500 caracteres"
            },
            Medio: {
                required: "El medio es obligatorio",
                maxlength: "La longitud máxima del medio es de 500 caracteres"
            },
            Accion_De_Prevencion: {
                required: "El trabajador es obligatorio",
                maxlength: "La longitud máxima del trabajador es de 500 caracteres"
            },
            FK_Probabilidad: {
                required: "Se debe seleccionar una probabilidad"
            },
            FK_Nivel_De_Deficiencia: {
                required: "Se debe seleccionar un nivel de deficiencia"
            },
            FK_Nivel_De_Exposicion: {
                required: "Se debe seleccionar un nivel de exposición"
            },
            FK_Consecuencia: {
                required: "Se debe seleccionar una consecuencia"
            },
            Numero_De_Expuestos: {
                required: "El número de expuestos es obligatorio",
                min: "Se debe ingresar mínimo un empleado expuesto",
                maxlength: "La longitud máxima del número de expuestos es de 5 caracteres"
            },
            Peor_Consecuencia: {
                required: "La peor consecuencia es obligatoria",
                maxlength: "La longitud máxima de la peor consecuencia es de 50 caracteres"
            },
            FLG_Requisito_Legal: {
                required: "Se debe seleccionar si hay requisito legal"
            },
            Accion_Requerida: {
                required: "Se debe ingresar una acción requerida",
                maxlength: "La longitud máxima de la acción requerida es de 1000 caracteres"
            },
            Responsable: {
                required: "Se debe ingresar un responsable",
                maxlength: "La longitud máxima del responsable es de 100 caracteres"
            },
            Fecha_Finalizacion: {
                required: "Se debe ingresar un responsable"
            },
            FK_ProbabilidadPersona: {
                required: "Se debe seleccionar una probabilidad"
            },
            FK_ProbabilidadClientes: {
                required: "Se debe seleccionar una probabilidad"
            },
            FK_ProbabilidadEconomica: {
                required: "Se debe seleccionar una probabilidad"
            },
            FK_ProbabilidadImagenE: {
                required: "Se debe seleccionar una probabilidad"
            },
            FK_ProbabilidadAmbiental: {
                required: "Se debe seleccionar una probabilidad"
            },
            consecuenciaPersona: {
                required: "Se debe seleccionar una consecuencia"
            },
            consecuenciasClientes: {
                required: "Se debe seleccionar una consecuencia"
            },
            consecuenciasEconomica: {
                required: "Se debe seleccionar una consecuencia"
            },
            consecuenciasImagenE: {
                required: "Se debe seleccionar una consecuencia"
            },
            consecuenciasAmbiental: {
                required: "Se debe seleccionar una consecuencia"
            },
            Procesos: {
                required: "Se debe seleccionar un proceso"
            },
            FK_Proceso: {
                required: "Se debe seleccionar un subproceso"
            },
            Eliminacion: {
                maxlength: "La longitud máxima de eliminación es de 2000 caracteres"
            },
            Sustitucion: {
                maxlength: "La longitud máxima de sustitución es de 2000 caracteres"
            },
            Controles_De_Ingenieria: {
                maxlength: "La longitud máxima de controles de ingeniería es de 2000 caracteres"
            },
            Controles_Administrativos: {
                maxlength: "La longitud máxima de controles administrativos es de 2000 caracteres"
            },
            Elementos_De_Proteccion: {
                maxlength: "La longitud máxima de elementos de protección es de 2000 caracteres"
            },
            Medidas_De_Control: {
                maxlength: "La longitud máxima de medidas de control es de 1000 caracteres"
            },
            Procedimientos_De_Trabajo: {
                maxlength: "La longitud máxima de procedimientos de trabajo es de 1000 caracteres"
            },
            Informacion: {
                maxlength: "La longitud máxima de información es de 1000 caracteres"
            },
            Formacion: {
                maxlength: "La longitud máxima de formación es de 1000 caracteres"
            },
            Consecuencias_Reales: {
                maxlength: "La longitud máxima de las consecuencias reales es de 500 caracteres"
            },
            Consecuencias_Potenciales: {
                maxlength: "La longitud máxima de las consecuencias potenciales es de 500 caracteres"
            },
            Riesgo_Controlado: {
                required: "Se debe seleccionar si el riesgo es controlado o no"
            }
        }

    });

}


function Validaciones_Formulario_requisitosLegales() {
    $("#RequisitosLegales").validate({
        // errorClass: "my-error-class",
        //errorPlacement: function (error, element) {
        //    utils.showMessage($(error).text(), "error", $(element).closest("div"), "bottom right");
        //    // error: es el label que contiene el error
        //    // element: el input el cual tiene el error de validación
        //},
        rules: {
            Sistema: {
                required: true
                // minlength: 2
            },
            FechaPublicacion: {
                required: true, date: true
                // minlength: 2
            },
            Ente: {
                required: true
                // minlength: 2
            },
            Articulo: {
                required: true
                // minlength: 2
            },
            Descripcion: {
                required: true
                // minlength: 2
            },
            Modificacion: {
                required: true
                // minlength: 2
            },
            Sugerencias: {
                required: true
                // minlength: 2
            },
            PartesInteresadas: {
                required: true
                // minlength: 2
            },
            Clase_De_Peligro: {
                required: true
                // minlength: 2
            },
            Peligro: {
                required: true
                // minlength: 2
            },

            Aspectos: {
                required: true
                // minlength: 2
            },
            Impactos: {
                required: true
                // minlength: 2
            },

            Impactos: {
                required: true
                // minlength: 2
            },
            Evidencia_Cumplimiento: {
                required: true
                // minlength: 2
            },
            FK_Cumplimiento_Evaluacion: {
                required: true
                // minlength: 2
            },

            Hallazgo: {
                required: true
                // minlength: 2
            },
            FK_Estado_RequisitoslegalesOtros: {
                required: true
                // minlength: 2
            },
            Responsable: {
                required: true
                // minlength: 2
            },

            Fecha_Seguimiento_Control: {
                required: true, date: true
                // minlength: 2
            },
            Fecha_Actualizacion: {
                required: true, date: true
                // minlength: 2
            },
            FK_Empresa: {
                required: true
                // minlength: 2
            }

        },
        messages: {
            Sistema: {
                required: "Nombre del sistema es obligatorio"
            },
            FechaPublicacion: {
                required: "Fecha Publicacion es obligatorio"
            },

            Ente: {
                required: "Campo Ente es obligatorio"
            },
            Articulo: {
                required: "Campo Artículo es obligatorio"
            },
            Descripcion: {
                required: "Campo Descripcion es obligatorio"
            },
            Modificacion: {
                required: "Campo Modificación es obligatorio"
            },
            Sugerencias: {
                required: "Campo Sugerencias es obligatorio"
            },
            PartesInteresadas: {
                required: "Campo Partes Interesadas es obligatorio"
            },
            Clase_De_Peligro: {
                required: "Campo Clase De Peligro es obligatorio"
            },
            Peligro: {
                required: "Campo Peligro es obligatorio"
            },
            Aspectos: {
                required: "Campo Aspectos es obligatorio"
            },
            Impactos: {
                required: "Campo Impactos es obligatorio"
            },

            Evidencia_Cumplimiento: {
                required: "Campo Evidencia Cumplimiento es obligatorio"
            },
            FK_Cumplimiento_Evaluacion: {
                required: "Campo Cumplimiento Evaluacion es obligatorio"
            },
            Hallazgo: {
                required: "Campo Hallazgo es obligatorio"
            },

            FK_Estado_RequisitoslegalesOtros: {
                required: "Campo Estado Requisitoslegales Otros es obligatorio"
            },
            Fecha_Seguimiento_Control: {
                required: "Fecha Seguimiento Control es obligatorio"
            },

            Responsable: {
                required: "Campo Responsable es obligatorio"
            },

            Fecha_Actualizacion: {
                required: "Fecha Actualizacion es obligatorio"
            }

        }

    });

}

function FormularioGenerarMetodologia() {
    $("#GenerarMetodologia").validate({
        rules: {
            Pk_Id_Sede: {
                required: true
            },
            PK_Metodologia: {
                required: true
            }
        },
        messages: {
            Pk_Id_Sede: {
                required: "Se debe seleccionar una sede"
            },
            PK_Metodologia: {
                required: "Se debe selecionar una metodologia"
            }
        }
    });
}

function MensajeGuardarRegistros() {
    utils.showMessage("Registro guardado con éxito", "error", $('#divMensaje'));
}




function validarPerfilSocioDemografico() {

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");


    $("#PerfilSocioDemografico").validate({

        rules: {
            PK_Numero_Documento_Empl: { required: true },
            Pk_Id_Sede: { required: true },
            IdMunicipio_Sede: { required: true },
            DepartamentoSede: { required: true },
            ZonaLugar: { required: true },
      
            GradoEscolaridad: { required: true },
            Ingresos: { required: true },
            Fk_Id_Departamento: { required: true },
            Fk_Id_Municipio: { required: true },

            FK_Etnia:{required:true},
            Conyuge: { required: true },
            Hijos: { required: true },

            FK_Estrato: { required: true },
            FK_Estado_Civil: { required: true },
        
            Ocupacion: { required: true },

            Sexo: { required: true },
            GrupoEtarios: { required: true },
            FK_VinculacionLaboral: { required: true },
            TurnoTrabajo: { required: true },
            Cargo: { required: true },
            fechaIngresoEmpresa: { required: true },
            FechaIngresoUltimoCargo: { required: true },
            eps:{required:true},
            afp:{required:true},
            FactorRiesgoPeligro: { required: true },

            caracteristicasFisicas:{required:true},
            caracteristicasPsicologicas: { required: true },
            evaluacionesMedicasRequeridas:{required:true},
            AntecedentesExpLaboral: { required: true },
            EvaluacionMedica: { required: true },

            Nombre1: {
                required: true,
                lettersonly: true
            },
            Apellido1: {
                required: true,
                lettersonly: true
            },
          
            Direccion: { required: true },
            Tipo_Documento: { required: true },
            ZonaLugar: { required: true },
        },
        messages: {

            IdMunicipio_Sede: {
                required: "El campo municipio es requerido"
            },
            DepartamentoSede: {
                required: "El campo departamento es requerido"
            },

            PK_Numero_Documento_Empl: {
                required: "Se debe ingresar el número de documento"
            },
            Pk_Id_Sede: {
                required: "Se debe seleccionar una sede"
            },
            ZonaLugar: {
                required: "Se debe seleccionar la zona o lugar"
            },

         
            GradoEscolaridad: {
                required: "Seleccione un grado de escolaridad "
            },
            Ingresos: {
                required: "Se debe seleccionar un tipo de ingreso "
            },
            Fk_Id_Departamento: {
                required: "Seleccione un departamento "
            },

            Fk_Id_Municipio: {
                required: "Seleccione un municipio "
            },

            Conyuge: {
                required: "Por favor seleccione una opción "
            },
            Hijos: {
                required: "Por favor seleccione una opción "
            },

            FK_Estrato: {
                required: "Se debe seleccionar un estrato "
            },

            FK_Estado_Civil: {
                required: "Se debe seleccionar un estado civil"
            },
            FK_Etnia: {
                required: "Se debe seleccionar la etnia"
            },
            Ocupacion: {
                required: "La ocupación es requerida"
            },


            Sexo: {
                required: "Seleccione su tipo de sexo"
            },
            GrupoEtarios: {
                required: "Seleccione el grupo etario"
            },
            FK_VinculacionLaboral: {
                required: "Seleccione su tipo de vinculación laboral"
            },
            TurnoTrabajo: {
                required: "Ingrese su turno de trabajo"
            },
            eps: {
                required: "La EPS es requerida"
            },
            afp: {
                required: "La AFP es requerida"
            },
            Cargo: {
                required: "El cargo es requerido"
            },
            fechaIngresoEmpresa: {
                required: "La fecha de ingreso a la empresa es obligatoria"
            },
            FechaIngresoUltimoCargo: {
                required: "Seleccione la fecha de ingreso a último cargo"
            },
            FactorRiesgoPeligro: {
                required: "Ingrese el factor de riesgo o peligro "
            },
            AntecedentesExpLaboral: {
                required: "Ingrese  los antecedentes a exposición laboral"
            },
            EvaluacionMedica: {
                required: "Ingrese  la evaluación médica"
            },
            Nombre1: {
                required: "El primer nombre es requerido"
            },
            Apellido1: {
                required: "El primer apellido es requerido"
            },
       
            Direccion: {
                required: "La dirección es requerida"
            },
            Tipo_Documento: {
                required: "Ingrese un documento"
            },
            ZonaLugar: {
                required: "Por favor seleccione una zona o lugar"
            },
            Otro: {
                required: "Por favor Ingrese otro Peligro"
            },
            evaluacionesMedicasRequeridas: {
                 required:"Por favor Ingrese las evaluaciones médicas requeridas"
            },
            caracteristicasPsicologicas: {
                required: "Por favor Ingrese las carácteristicas psicologicas"
            },
            caracteristicasFisicas: {
                required: "Por favor Ingrese las carácteristicas físicas"
            },




        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "GradoEscolaridad") {

                error.appendTo("#errorGradoEscolaridad");
            } else if (element.attr("name") == "Ingresos") {

                error.appendTo("#errorIngresos");

            } else if (element.attr("name") == "Conyuge") {

                error.appendTo("#errorConyuge");

            } else if (element.attr("name") == "Hijos") {

                error.appendTo("#errorHijos");

            } else if (element.attr("name") == "Sexo") {

                error.appendTo("#errorSexo");

            }

            else {
                error.insertAfter(element);
            }


        }



    });

    $("#modalactualizarp").modal('hide');
}


$(document).ready(function () {


    // Write on keyup event of keyword input element
    $("#buscar").keyup(function () {
        // When value of the input is not blank
        if ($(this).val() != "") {
            // Show only matching TR, hide rest of them
            $("#select_perfil tbody>tr").hide();
            $("#select_perfil td:contains-ci('" + $(this).val() + "')").parent("tr").show();
        }
        else {
            // When there is no input or clean again, show everything back
            $("#select_perfil tbody>tr").show();
        }
    });

    // jQuery expression for case-insensitive filter
    $.extend($.expr[":"],
    {
        "contains-ci": function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || $(elem).text() || "").toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });

    $("#actRegistro").click(function () {

        var form = $("#PerfilSocioDemografico");
        if (!$("#PerfilSocioDemografico").valid()) {
            validacion = false;
        }
        else {
            validacion = true;
        }


        if (validacion == false) {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Faltan campos por llenar',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        var fechaIE = $("#fechaIngresoEmpresa").val();
        var fechaUltimoCargo = $("#FechaIngresoUltimoCargo").val();
        var form = $("#PerfilSocioDemografico");
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();

        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaUltimoCargo)) {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de último cargo no puede ser superior a la fecha actual',
                confirmButtonColor: '#7E8A97'
            });


            return false;
        }

        if ($.datepicker.parseDate('dd/mm/yy', fechaUltimoCargo) < $.datepicker.parseDate('dd/mm/yy', fechaIE)) {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de ingreso del último cargo, no puede ser menor que  la fecha de ingreso a la empresa',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        var validarOtroPeligro = true;
        var validandoTexpo = true;
        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {



            var $tiempoE = $(fila).closest("tr").find("input[name = tiempoExpos]");
            var $fcla = $(fila).closest("tr").find("select[name = FK_Clasificacion_De_Peligro]");
            var $otro = $(fila).closest("tr").find("input[name = Otro]");


            if ($otro.is(":Visible")) {

                if ($otro.val() == "") {


                    validarOtroPeligro = false;


                }
            }

            if ($tiempoE.val() == "" ) {

                validandoTexpo = false;
            }
        });


        if (validandoTexpo == false) {


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Los campos de la exposición a peligro son obligatorios',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        if (validarOtroPeligro == false) {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Por favor ingrese otro tipo de peligro',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }



        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {

            //$(fila).find("#Pk_Id_Sede").attr("name", "[" + ind + "].Pk_Id_Sede");
            //$(fila).find("#ZonaLugar").attr("name", "[" + ind + "].ZonaLugar");
            $(fila).find("#FK_Tipo_De_Peligro").attr("name", "[" + ind + "].FK_Tipo_De_Peligro");
            $(fila).find("#tiempoExpos").attr("name", "[" + ind + "].tiempoExpos");
            $(fila).find("#FK_Clasificacion_De_Peligro").attr("name", "[" + ind + "].FK_Clasificacion_De_Peligro");
            $(fila).find("#Otro").attr("name", "[" + ind + "].Otro");
        });



        if (validacion) {
            form.submit();
        }


    });

    $("#actRegistroEditado").click(function () {
        var form = $("#PerfilSocioDemografico");
        if (!$("#PerfilSocioDemografico").valid()) {
            validacion = false;
        }
        else {
            validacion = true;
        }

        if (validacion == false) {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Falta campos por llenar',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }
        var fechaIE = $("#fechaIngresoEmpresa").val();
        var fechaUltimoCargo = $("#FechaIngresoUltimoCargo").val();
        var form = $("#PerfilSocioDemografico");
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();

        if ($("#Otro").is(":Visible")) {
            var otro = $("#Otro").val();
            if (otro == "") {
                swal({
                    type: 'warning',
                    title: 'Estimado usuario:',
                    text: 'Por favor ingrese otro tipo de peligro',
                    confirmButtonColor: '#7E8A97'
                });

                return false;
            }
        }
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {

            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaUltimoCargo)) {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de último cargo no puede ser superior a la fecha actual',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        if ($.datepicker.parseDate('dd/mm/yy', fechaUltimoCargo) < $.datepicker.parseDate('dd/mm/yy', fechaIE)) {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de ingreso del último cargo, no puede ser menor que  la fecha de ingreso a la empresa',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        if ($.datepicker.parseDate('dd/mm/yy', fecha_actual) < $.datepicker.parseDate('dd/mm/yy', fechaUltimoCargo)) {


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'La fecha de ingreso del último cargo,  no puede ser superior a la fecha actual',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }
        var validarOtroPeligro = true;
        var validandoTexpo = true;
        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {

            //var $tiempoE = $(fila).closest("tr").find("input[name = tiempoExpos]");
            //var $fcla = $(fila).closest("tr").find("select[name = FK_Clasificacion_De_Peligro]");
            //var $fclaE = $(fila).closest("tr").find("select[name = FK_Clasificacion_De_PeligroE]");

            //if ($tiempoE.val() == "" ) {

            //    validandoTexpo = false;
            //}
            var $fclaE = $(fila).closest("tr").find("select[name = FK_Clasificacion_De_PeligroE]");
            var $tiempoE = $(fila).closest("tr").find("input[name = tiempoExpos]");
            var $fcla = $(fila).closest("tr").find("select[name = FK_Clasificacion_De_Peligro]");
            var $otro = $(fila).closest("tr").find("input[name = Otro]");


            if ($otro.is(":Visible")) {

                if ($otro.val() == "") {


                    validarOtroPeligro = false;


                }
            }

            if ($tiempoE.val() == "") {

                validandoTexpo = false;
            }
        });

        if (validandoTexpo == false) {


            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Los campos de la exposición a peligro son obligatorios',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }

        if (validarOtroPeligro == false) {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: 'Por favor ingrese otro tipo de peligro',
                confirmButtonColor: '#7E8A97'
            });

            return false;
        }


        $filasAgrupadas = $("#actividades").find("tr");
        //var indexFila = 0;
        $filasAgrupadas.each(function (ind, fila) {

            //$(fila).find("#Pk_Id_Sede").attr("name", "[" + ind + "].Pk_Id_Sede");
            //$(fila).find("#ZonaLugar").attr("name", "[" + ind + "].ZonaLugar");
            $(fila).find("#FK_Tipo_De_Peligro").attr("name", "[" + ind + "].FK_Tipo_De_Peligro");
            $(fila).find("#tiempoExpos").attr("name", "[" + ind + "].tiempoExpos");
            $(fila).find("#FK_Clasificacion_De_Peligro").attr("name", "[" + ind + "].FK_Clasificacion_De_Peligro");
            $(fila).find("#FK_Clasificacion_De_PeligroE").attr("name", "[" + ind + "].FK_Clasificacion_De_PeligroE");
            $(fila).find("#PKCondicionesRiesgoPerfil").attr("name", "[" + ind + "].PKCondicionesRiesgoPerfil");

            $(fila).find("#Otro").attr("name", "[" + ind + "].Otro");
        });


        if (validacion) {
            form.submit();

        }

        


    });

    //funcion para ocupacion del perfil socio demogr
    $(document).ready(function () {

        $("#Ocupacion").autocomplete({
            minLength: 2,
            source: function (request, response) {
                $.ajax({
                    url: urlBase + urlPerfil + "/AutoCompletarOcupacion",
                    type: "POST",
                    dataType: "json",
                    data: { prefijo: request.term },
                }).done(function (data) {
                    response($.map(data, function (item) {
                        return { label: item.codigo + "-" + item.grupoPrimario, value: item.PKOcupacionPerfil };
                    }))
                })
            },
            focus: function (event, ui) {
                event.preventDefault();
                $(this).val(ui.item.label);
            },
            select: function (event, ui) {
                event.preventDefault();
                $(this).val(ui.item.label);
                $("#PKOcupacionPerfil").val(ui.item.value);
            }
        });
    });

});


//funcion para formatear el texto de los mensajes de Jquery Validate
$(document).ready(function () {
    $.extend(jQuery.validator.messages, {
        minlength: $.validator.format("Debe ingresar mínimo {0} caracteres"),

    });
})

function validarRequisitoslegalesModificarRequisitoMatriz() {

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");

    $("#idModificarRequisitoMatriz").validate({
        errorClass: "error",
        rules: {


            Tipo_Norma: { required: true, maxlength: 50 },
            Numero_Norma: { required: true, maxlength: 50 },
            FechaPublicacion: { required: true },
            Ente: { required: true, maxlength: 20 },
            Articulo: { required: true, maxlength: 20 },
            Descripcion: { required: true, maxlength: 100 },
            Sugerencias: { required: true, maxlength: 50 },

            Clase_De_Peligro: { required: true, maxlength: 20 },
            Peligro: { required: true, maxlength: 20 },
            Aspectos: { required: true },
            Impactos: { required: true },


            Evidencia_Cumplimiento: { required: true },
            FK_Cumplimiento_Evaluacion: { required: true },
            Hallazgo: { required: true },
            FK_Estado_RequisitoslegalesOtros: { required: true },
            Responsable: { required: true },
            Fecha_Seguimiento_Control: { required: true, },
            Fecha_Actualizacion: { required: true }

        },
        messages: {

            //Tipo_Norma: {
            //    required: "Se debe ingresar el tipo de norma",
            //    maxlength: "La longitud máxima es de 50 caracteres",
            //},
            //Numero_Norma: {
            //    required: "Se debe ingresar el numero norma",
            //    maxlength: "La longitud máxima del numero es de 50 caracteres",
            //},
            //FechaPublicacion: {
            //    required: "Se debe ingresar la fechaPublicacion"
            //},

            //Ente: {
            //    required: "Se debe ingresar el ente correspondiente",
            //    maxlength: "La longitud máxima es de 20 caracteres",
            //},
            //Articulo: {
            //    required: "Se debe ingresar el articulo correspondiente",
            //    maxlength: "La longitud máxima es de 20 caracteres",
            //},

            //Descripcion: {
            //    required: "Se debe ingresar la descripcion",
            //    maxlength: "La longitud máxima es de 100 caracteres",
            //},

            //Sugerencias: {
            //    required: "Se deben ingresar las sugerencias",
            //    maxlength: "La longitud máxima es de 50 caracteres",
            //},

            //Clase_De_Peligro: {
            //    required: "Se debe ingresar la clase de peligro",
            //    maxlength: "La longitud máxima es de 20 caracteres",
            //},

            //Peligro: {
            //    required: "Se debe ingresar el peligro",
            //    maxlength: "La longitud máxima es de 20 caracteres",
            //},

            //Aspectos: {
            //    required: "Se debe ingresar el aspecto"
            //},
            //Impactos: {
            //    required: "Se debe ingresar el impacto"
            //},


            //Evidencia_Cumplimiento: {
            //    required: "Se debe ingresar la evidencia"
            //},
            //FK_Cumplimiento_Evaluacion: {
            //    required: "Se debe ingresar el cumplimiento"
            //},
            //Hallazgo: {
            //    required: "Se debe ingresar el hallazgo"
            //},

            //FK_Estado_RequisitoslegalesOtros: {
            //    required: "Se debe ingresar el estado del requisito legal"
            //},
            //Responsable: {
            //    required: "Se debe ingresar el responsable"
            //},

            //Fecha_Seguimiento_Control: {
            //    required: "Se debe ingresar la fecha de seguimiento del control"
            //},

            //Fecha_Actualizacion: {
            //    required: "Se deben ingresar la fecha de actualizacion"
            //}




        }

    });

}




function mostrarProceso() {
    if ($("#IdProcesoCargue").is(":visible")) {
        $("#IdProcesoCargue").hide();
    }
    else {
        $("#IdProcesoCargue").show();
    }

}


function mostrarSedes() {
    if ($("#idSede").is(":visible")) {
        $("#idSede").hide();
    }
    else {
        $("#idSede").show();
    }

}


$('#agregarCondiesgoDeRiesgo').on('click', function () {
    tabla = $('#TablaRiesgo');
    tr = $("#filaRiesgoClonada");
    if (contCondicionesRiesgo <= 2) {

        tr.clone().appendTo(tabla).find(':text').val('');

        contCondicionesRiesgo++;
    }
    else {


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'No se permite el ingreso de más de tres exposiciones de peligro.',
            confirmButtonColor: '#7E8A97'
        });

    }


});

$('#agregarCondiesgoDeRiesgoEditado').on('click', function () {
    tabla = $('#TablaRiesgo');
    tr = $("#filaRiesgoClonar");



    var contCondicionesRiesgoEditado = 0;
    $filasAgrupadas = $("#actividades").find("tr");
    //var indexFila = 0;
    $filasAgrupadas.each(function (ind, fila) {
        contCondicionesRiesgoEditado++;
    });
    if (contCondicionesRiesgoEditado <= 2) {

        tr.clone().appendTo(tabla).find(':text').val('');


    }
    else {

        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'No se permite el ingreso de más de tres exposiciones de peligro.',
            confirmButtonColor: '#7E8A97'
        });

    }





});


function eliminarCondicionDeRiesgo(event) {

    if (contCondicionesRiesgo > 1) {
        $(event).closest('tr').remove();
        contCondicionesRiesgo--;
    }
    else {
       // swal("Estimado Usuario", "No se puede eliminar, se debe tener una exposición de peligros",'warning','#7E8A97');
        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'No se puede eliminar, se debe tener una exposición de peligros',

            confirmButtonColor: '#7E8A97'
        });


    }


 

}

function eliminarCondicionDeRiesgoEditado(event) {

    tabla = $('#TablaRiesgo');
    tr = $("#filaRiesgo");
    var contCondicionesRiesgoEditado = 0;
    $filasAgrupadas = $("#actividades").find("tr");
    //var indexFila = 0;
    $filasAgrupadas.each(function (ind, fila) {
        contCondicionesRiesgoEditado++;
    });


    if (contCondicionesRiesgoEditado > 1) {
        $(event).closest('tr').remove();

    }
    else {
        // swal("Estimado Usuario", "No se puede eliminar, se debe tener una exposición de peligros",'warning','#7E8A97');
        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'No se puede eliminar, se debe tener una exposición de peligros',

            confirmButtonColor: '#7E8A97'
        });


    }
}



function cambiarCuadroDialogoOtro(event)
{
    var $inputOtro = $("#Otro")
    var $tipoP = $(event).closest("tr").find("input[id =item_descripcionPeligro]");
    var $desp = $(event).closest("tr").find("input[id =item_ClasificacionPeligro]");
    var $fclas = $(event).closest("tr").find("select[id =FK_Clasificacion_De_Peligro]");

    //var $otro = $(event).closest("tr").find("input[id =Otro]");
    var $inputOtro = $(event).closest("tr").find("input[name = Otro]");
    if ($tipoP.val() == "Otro")
    {
       
        $desp.hide();
        $inputOtro.attr("type", "text");
     
    }
    else
    {
        $fclas.removeAttr("hidden");
    }

  

}





 //uncion para consultar las clases de peligro por tipo de peligro
function ConsultarClasesPeligrosPerfil(selectPeligro) {
    var $inputOtro = $("#Otro");
    var $inputOtro = $(selectPeligro).closest("tr").find("input[name = Otro]");
    var $selectClasesP = $(selectPeligro).closest("tr").find("select[name = FK_Clasificacion_De_Peligro]");


    //var $divselectClasesP = $("#divSelectClas");
    var $divselectClasesP = $(selectPeligro).closest("tr").find("div[id = divSelectClas]");
    if (IDOTRO == $(selectPeligro).val()) {
        $inputOtro.attr("type", "text");
        $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
        $selectClasesP.append(new Option("Otro", 46));//agregamos la opcion de otro, el numero 46 hace referencia al id en la base de datos de la clasificacion otro
        $divselectClasesP.attr("hidden", "hidden");
    }
    else {
        $inputOtro.attr("type", "hidden");
        $divselectClasesP.removeAttr("hidden");
        $.ajax({
            url: urlBase + urlPerfil + '/ConsultarClasesPeligrosPerfil',
            data: {
                Pk_Tipo_Peligro: $(selectPeligro).val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {

                    $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
                    $selectClasesP.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar                

                    $.each(result, function (ind, element) {



                        $selectClasesP.append(new Option(element.ClasesDescription, element.PK_ClasesPeligros));//agregamos las opciones consultadas

                    });


                }
            }
        });
    }

}


// funcion para consultar las clases de peligro por tipo de peligro
function ConsultarClasesPeligrosPerfilEditado(selectPeligro) {


    // var $inputOtro = $("#Otro");
    var $inputOtro = $(selectPeligro).closest("tr").find("input[name = Otro]");
    var $selectClasesP = $(selectPeligro).closest("tr").find("select[name = FK_Clasificacion_De_Peligro]");

    //var $divselectClasesP = $("#divSelectClas");
    var $divselectClasesP = $(selectPeligro).closest("tr").find("div[id = divSelectClas]");
    if (IDOTRO == $(selectPeligro).val()) {
        $inputOtro.attr("type", "text");
        $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
        $selectClasesP.append(new Option("Otro", 46));//agregamos la opcion de otro, el numero 46 hace referencia al id en la base de datos de la clasificacion otro
        $divselectClasesP.hide();
        $inputOtro.removeAttr("type", "hidden");



    }
    else {
        $inputOtro.attr("type", "hidden");
        //$divselectClasesP.removeAttr("hidden");
        $.ajax({
            url: urlBase + urlPerfil + '/ConsultarClasesPeligrosPerfil',
            data: {
                Pk_Tipo_Peligro: $(selectPeligro).val()
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $selectClasesP.find("option").remove();//Removemos las opciones anteriores 
                    $selectClasesP.append(new Option("Seleccionar", ""));// agregamos la opcion de seleccionar                

                    $.each(result, function (ind, element) {
                        $selectClasesP.append(new Option(element.ClasesDescription, element.PK_ClasesPeligros));//agregamos las opciones consultadas

                    });


                }
            }
        });
    }
}



function mostrarClaseDePeligros(selectPeligro)
{
    var $tipoP = $(selectPeligro).closest("tr").find("div[id = clasesDePeligro]");

    var $desp = $(selectPeligro).closest("tr").find("div[id = divSelectClas]");
    var $fclas = $(event).closest("tr").find("select[name =FK_Tipo_De_Peligro]");


    var $removerPeligro = $(selectPeligro).closest("tr").find("input[id = item_descripcionPeligro]");
    var $removerDescripcion = $(selectPeligro).closest("tr").find("input[id = item_ClasificacionPeligro]");
    //$selectClasesP.removeAttr("hidden");

    $tipoP.show();
    $desp.show();
    $removerPeligro.hide();
    $removerDescripcion.hide();


    
    

}



function ObtenerSiarp2() {

    utils.showLoading();
    $.ajax({
        url: urlBase + '/PerfilSocioDemografico/ConsultarDatosTrabajador',  //primero el modulo/controlador/metodo que esta en el controlador
        data: {                                           // se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Documento: $("#PK_Numero_Documento_Empl").val()
        },
        type: 'POST',
    }).done(function (result) {
        utils.closeLoading();
        if (result.Mensaje == 'OK') {
            $("#formPlan").removeAttr('hidden');
            $("#btnFormPerfil").removeAttr('hidden');
            $("#exportandoExcel").hide();
            $("#divFormulario").hide();

            $("#idtipodoc").val(result.Data[0].tipoDoc);
            $("#idtipodoc").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;



            $("#PK_Numero_Documento_Empl").val(result.Data[0].idPersona);
            $("#PK_Numero_Documento_Empl").css("background-color", "whitesmoke")
                .css("color", "#black").css("border", "groove");
            $("#PK_Numero_Documento_Empl").prop("disabled", false);

            $("#Nombre1").val(result.Data[0].nombre1);
            $("#Nombre1").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

            $("#Nombre2").val(result.Data[0].nombre2);
            $("#Nombre2").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

            $("#Apellido1").val(result.Data[0].apellido1);
            $("#Apellido1").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

            $("#Apellido2").val(result.Data[0].apellido2);
            $("#Apellido2").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;

            $("#Direccion").val(result.Data[0].dirPersona);
            $("#Direccion").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;



            $("#OcupacionPerfil").val(result.Data[0].ocupacion);
            $("#OcupacionPerfil").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;



            $("#Cargo").val(result.Data[0].cargo);
            $("#Cargo").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;




            $("#Cargo").val(result.Data[0].cargo);
            $("#Cargo").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;


            $("#afp").val(result.Data[0].nombreAfp);
            $("#afp").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;


            $("#eps").val(result.Data[0].nombreEps);
            $("#eps").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;







            var fecha2 = result.Data[0].fechaInicioVinculacion;

            var values = fecha2.split("-");
            var dia = values[2];

            var mes = values[1];
            var anyo = values[0];
            var diaT = dia.substr(0, 2);


            var fecha_Ing_Emp = String(diaT + "/" + mes + "/" + anyo);
            $("#fechaIngresoEmpresa").val(fecha_Ing_Emp);
            $("#fechaIngresoEmpresa").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;



            // Obtenemos los valores actuales
            var fecha_hoy = new Date();
            var ahora_anyo = fecha_hoy.getYear();
            var ahora_mes = fecha_hoy.getMonth() + 1;
            var ahora_dia = fecha_hoy.getDate();

            var anyos = (ahora_anyo + 1900) - anyo;
            if (anyos != 0) {
                if (ahora_mes < mes) {
                    anyos--;
                }
                if ((mes == ahora_mes) && (ahora_dia < diaT)) {
                    anyos--;
                }
                if (anyos > 1900) {
                    anyos -= 1900;
                }
            }

            // calculamos los meses
            var meses = 0;
            if (ahora_mes > mes) {
                meses = ahora_mes - mes;
            }
            if (ahora_mes > mes && ahora_dia >= diaT) {
                meses = ahora_mes - mes;
            }
            if (ahora_mes < mes) {
                meses = 12 - (mes - ahora_mes);
            }
            if (ahora_mes > mes && ahora_dia < diaT) {
                meses--;
            }
            if (ahora_mes == mes && diaT > ahora_dia) {
                meses = 11;
            }

            // calculamos los dias
            var dias = 0;
            if (ahora_dia > diaT)
                dias = ahora_dia - diaT;
            if (ahora_dia < diaT) {
                ultimoDiaMes = new Date(ahora_anyo, ahora_mes, 0);
                dias = ultimoDiaMes.getDate() - (diaT - ahora_dia);
            }

            $("#anyos").val(anyos);
            $("#meses").val(meses);
            $("#dias").val(dias);



            //fecha nacimiento

            var fechaNac = result.Data[0].fechaNacimiento;


            var values = fechaNac.split("-");
            if (values == "") {
                values = fechaNac.split("/");
            }
            var anyo = values[0];
            var dia = values[2];
            var mes = values[1];

            var fecha_hoy = new Date();
            var ahora_anyo = fecha_hoy.getYear();
            var ahora_mes = fecha_hoy.getMonth() + 1;
            var ahora_dia = fecha_hoy.getDate();
            // Obtenemos los valores actuales
            var fecha_hoy = new Date();
            var ahora_anyo = fecha_hoy.getYear();
            var edad = (ahora_anyo + 1900) - anyo;
            if (edad != 0) {
                if (ahora_mes < mes) {
                    edad--;
                }
                if ((mes == ahora_mes) && (ahora_dia < dia)) {
                    edad--;
                }
                if (edad > 1900) {
                    edad -= 1900;
                }
            }

            $("#idFechaNacimiento").val(edad);

            if (edad <= 25) {
                $("input[name=Grupo][value='Menores de 18 años a 25 años']").prop("checked", true);
                $("#GrupoEtarios").val('Menores de 18 años a 25 años');
            }
            else if (edad >= 26 && edad <= 35) {

                $("input[name=Grupo][value='26 a 35 años']").prop("checked", true);
                $("#GrupoEtarios").val("26 a 35 años");

            }
            else if (edad >= 36 && edad <= 45) {
                $("input[name=Grupo][value='36 a 45 años']").prop("checked", true);
                $("#GrupoEtarios").val("36 a 45 años");

            }
            else if (edad >= 46 && edad <= 55) {
                $("input[name=Grupo][value='46 a 55 años']").prop("checked", true);
                $("#GrupoEtarios").val("46 a 55 años");
            }
            else {
                $("input[name=Grupo][value='Mayores a los 55 Años']").prop("checked", true);
                $("#GrupoEtarios").val("Mayores a los 55 Años");

            }
            $("#idFechaNacimiento").css("background-color", "whitesmoke").css("color", "#black").css("border", "groove");;
                
        } else if (result.Mensaje == 'ERROR') {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
          
        }
        else if (result.Mensaje == 'EXISTE') {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
        else if (result.Mensaje == 'CONEXION') {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
        else if (result.Mensaje == 'NULO') {

            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: result.Data,

                confirmButtonColor: '#7E8A97'
            });
        }
    }).fail(function (result) {
        console.log("Error en la peticion: " + result.Data,'warning');
    });
};



$('#idDescargarReporte').click(function () {


    PopupPosition();

    $.ajax({
        type: "POST",
        data: "",

        url: urlBase + urlPerfil + '/ExportarReportePerfilExcel'
    }).done(function (response) {
        if (response != undefined && response.Mensaje == 'Success') {
      
        }
    });
});

$('#idUpPlantillaPerfilSocioDemografico').click(function () {

    var form_data = new FormData();
    var filedata = $("#file").prop("files")[0];
    if (filedata != undefined)
        form_data.append("cargarArchivo", filedata);
    else {
    


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'Debe seleccionar un archivo',

            confirmButtonColor: '#7E8A97'
        });
        return
    }
    PopupPosition();

    $.ajax({
        type: "POST",
        data: form_data,
        url: urlBase + urlPerfil + '/CargueMasivo',
        processData: false,
        dataType: 'json',
        contentType: false
    }).done(function (response) {
        if (response != undefined && response.Mensaje == 'Success') {
            swal({
                type: 'success',
                title: 'Estimado usuario:',
                text: response.Data,

                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
            $("#listadoCargue").show();
        }
        else if (response != undefined && response.Mensaje == 'ERROR') {
            swal({
                type: 'warning',
                title: 'Estimado usuario:',
                text: response.Data,

                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else if (response != undefined && response.Mensaje == 'CONEXION') {
            swal("Estimado Usuario",
                  response.Data,
                  'warning');


        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text:  response.Data,

            confirmButtonColor: '#7E8A97'
        });
            OcultarPopupposition();
        }
    }).fail(function (response) {
        $("#Documento").val('');
        swal("Estimado Usuario",
            "No se logró obtener datos del trabajador. Intente más tarde.",
            'warning');
        OcultarPopupposition();
    });
});




function desahabilitarRadioGrupoEtareo() {
    jQuery("input[name='Grupo']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });
}



function darFormatoSoloNumerosPer(tiempoExpos) {

    var $id = $(tiempoExpos).closest("tr").find("input[name = tiempoExpos]");
    $id.on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}

//funcion que me permite eliminar la exposicion al reporte
function EliminarExposSelec(element, IDExposicionPerfil) {
    tabla = $('#TablaRiesgo');
    tr = $("#filaRiesgo");
    var contCondicionesRiesgoEditado = 0;
    $filasAgrupadas = $("#actividades").find("tr");
    //var indexFila = 0;
    $filasAgrupadas.each(function (ind, fila) {
        contCondicionesRiesgoEditado++;
    });


    if (contCondicionesRiesgoEditado > 1) {
        $div = $(element).closest("tr");
        $.ajax({
            url: urlBase + urlPerfil + '/EliminarExposicion',
            data: {
                idExposicion: IDExposicionPerfil
            },
            type: 'POST',
            success: function (result) {

                if (result.success) {
                    $("#modalesEliminados").append($("#modalEliminar" + IDExposicionPerfil));
                    $div.remove();
                    swal(
                        'Estimado Usuario',
                        'Se ha eliminado la exposición',
                        'success'
                        )
                } else {
                    swal(
                       'Estimado Usuario',
                       'No ha sido posible eliminar la exposición',
                       'warning'
                       )
                }
            }
        });
    } else {

        swal({
            type: 'warning',
            title: 'Estimado usuario:',
            text: 'No se puede eliminar, se debe tener una exposición de peligros',

            confirmButtonColor: '#7E8A97'
        });

    }


}