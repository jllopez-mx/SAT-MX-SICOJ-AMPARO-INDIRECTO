namespace AmparoIndirectoAPI.Model.ViewModels.Enums
{
    public static class EnumFunctions
    {
        #region Oficial de Partes

        #region

        public const string FechasVencimientoSeccionGetAll = "amparo_indirecto.fn_get_all_fecha_vencimiento"; //agregado
        public const string FechasVencimientoUpdate= "amparo_indirecto.fn_update_fecha_vencimiento";


        #endregion

        #region Control Documental
        public const string CreateAsuntoControlDocumental = "amparo_indirecto.fn_op_create_asunto_control_documental";
        public const string ConsultaAsuntoControlDocumental = "amparo_indirecto.fn_read_asunto_control_documental";

        #endregion

        #region Crear Amparo Indirecto
        public const string AmparoIndirectoCreateRegistro = "amparo_indirecto.fn_op_create_amparo_indirecto";
        public const string AmparoIndirectoUpdateRegistro = "amparo_indirecto.fn_op_update_amparo_indirecto";
        public const string AmparoIndirectoUpdateDeleteRegistro = "amparo_indirecto.fn_op_delete_amparo_indirecto";
        public const string AmparoIndirectoGetById = "amparo_indirecto.fn_read_amparo_by_id";  
        #endregion

        #region Bandeja
        public const string AmparoIndirectoGetAll = "amparo_indirecto.fn_op_bandeja_pendientes";
        public const string AmparoIndirectoCount = "amparo_indirecto.fn_op_bandeja_pendientes_count";
        #endregion

        #region Historico
        public const string AmparoIndirectoByFilters = "amparo_indirecto.fn_op_historico_asuntos";
        public const string AmparoIndirectoByFiltersCount = "amparo_indirecto.fn_op_historico_asuntos_count";
        #endregion

        #region Turnar
        public const string AmparoIndirectoUpdateTurnar = "amparo_indirecto.fn_op_update_turnar";
        #endregion

        #region Solicitud de Transparencia
        public const string SolicitudTransparenciaCreate = "amparo_indirecto.fn_op_create_solicitud_transparencia";
        public const string SolicitudTransparenciaSelect = "amparo_indirecto.fn_op_get_solicitud_transparencia";
        public const string SolicitudTransparenciaCount = "amparo_indirecto.fn_op_get_solicitud_transparencia_count";
        public const string SolicitudTransparenciaByID = "amparo_indirecto.fn_op_get_solicitud_transparencia_by_id";
        public const string SolicitudTransparenciaDelete = "amparo_indirecto.fn_op_delete_solicitud_transparencia";
        #endregion

        #region Acumular
        public const string AcumularJuicioCreate = "amparo_indirecto.fn_op_create_acumular";
        public const string AcumularSelect = "amparo_indirecto.fn_op_get_juicios_a_acumular";
        public const string AcumularSelectCount = "amparo_indirecto.fn_op_get_juicios_a_acumular_count";
        public const string AcumularCabezaSerie = "amparo_indirecto.fn_op_get_acumular_cabeza_serie";
        #endregion

        #region Declinar
        public const string DeclinarCompetenciaCreateRegistro = "amparo_indirecto.fn_op_create_declinar_competencia";
        public const string DeclinarCompetenciaCount = "amparo_indirecto.fn_get_declinar_competencia_count";  //falta
        public const string DeclinarCompetenciaGetAll = "amparo_indirecto.fn_get_declinar_competencia";       //falta
        public const string DeclinarCompetenciaDelete = "amparo_indirecto.fn_delete_declinar_competencia";    //falta
        #endregion

        #region Canalizar
        public const string CanalizarAsuntoCreate = "amparo_indirecto.fn_op_create_canalizar_asunto";
        public const string CanalizarGetAll = "amparo_indirecto.fn_op_get_canalizar_asunto";
        public const string CanalizarGetCount = "amparo_indirecto.fn_op_get_canalizar_asunto_count";
        #endregion

        #region Asignar Abogado
        public const string AmparoIndirectoAsignarAbogadoOficialPartes = "amparo_indirecto.fn_op_update_asigna_abogado";
        #endregion

        #endregion


        #region Administrador

        public const string AmparoIndirectoGetByIds = "amparo_indirecto.fn_read_amparo_by_ids"; //duda

        public const string BandejaAdmGetAll = "amparo_indirecto.fn_adm_bandeja_pendientes";
        public const string BandejaAdmGetAllCount = "amparo_indirecto.fn_adm_bandeja_pendientes_count";

        public const string HistoricoAdmByFilters = "amparo_indirecto.fn_adm_historico_asuntos";
        public const string HistoricoAdmByFiltersCount = "amparo_indirecto.fn_adm_historico_asuntos_count";

        public const string UpdateRegistroAdministrador = "amparo_indirecto.fn_adm_update_amparo_indirecto_administrador";
        public const string CreateReasignarAbogadoAdministrador = "amparo_indirecto.fn_adm_update_reasignar_abogado_administrador_ids";
        public const string AsignarAbogadoAdministrador = "amparo_indirecto.fn_adm_update_asignar_abogado_administrador";
        //public const string CreateReasignarAbogadoAdministrador = "amparo_indirecto.fn_update_reasignar_abogado_administrador";

        #endregion


        #region Abogado
        public const string BandejaAbogadoGetAll = "amparo_indirecto.fn_abo_bandeja_pendientes";
        public const string BandejaAbogadoGetAllCount = "amparo_indirecto.fn_abo_bandeja_pendientes_count";

        public const string HistoricoAbogadoByFilters = "amparo_indirecto.fn_abo_historico_asuntos";
        public const string HistoricoAbogadoByFiltersCount = "amparo_indirecto.fn_abo_historico_asuntos_count";

        public const string CreateSuspensionProvisional = "amparo_indirecto.fn_abo_create_suspension_provisional_incidental";
        public const string GetAllSuspensionProvisional = "amparo_indirecto.fn_abo_get_suspension_provisional_incidental";

        public const string CreateAutoridadesResponsables = "amparo_indirecto.fn_abo_create_autoridades_responsables";
        public const string AutoridadesResponsables = "amparo_indirecto.fn_abo_read_autoridades_responsables_by_id";
        public const string AutoridadesResponsablesDisconect = "amparo_indirecto.fn_abo_read_autoridades_responsables_by_id_interno";
        public const string AutoridadesResponsablesRealesDisconect = "amparo_indirecto.fn_abo_get_autoridades_recurso_revision_constitucional";

        public const string GetAllInformacionAdicional = "amparo_indirecto.fn_abo_get_informacion_adicional";
        public const string CreateInformacionAdicional = "amparo_indirecto.fn_abo_create_informacion_adicional";
        public const string CreateNotaLitigio = "amparo_indirecto.fn_abo_create_nota_litigio";
        public const string UpdateDeleteNotaLitigio = "amparo_indirecto.fn_abo_delete_nota_litigio";
        public const string GetNotaLitigio = "amparo_indirecto.fn_abo_read_nota_litigio_by_id";

        public const string InformePrevioIncidentalCreate = "amparo_indirecto.fn_abo_create_informe_previo_incidental";

        public const string InformePrevioIncidentalUpdate = "amparo_indirecto.fn_abo_update_estado_procesal_informe_previo_incidental";
        public const string InformePrevioIncidentalGetAll = "amparo_indirecto.fn_abo_get_informe_previo_incidental";

        public const string SentenciaIncidentalCreate = "amparo_indirecto.fn_abo_create_sentencia_incidental";
        public const string InformeSentenciaIncidentalUpdate = "amparo_indirecto.fn_abo_update_estado_procesal_sentencia_incidental";
        public const string SentenciaIncidentalGetAll = "amparo_indirecto.fn_abo_get_sentencia_incidental";

        public const string RecursoQuejaIncidentalAutoriadesResponsablesCreate = "amparo_indirecto.fn_abo_create_recurso_queja_autoridades_incidental";
        public const string RecursoQuejaIncidentalQuejosoCreate = "amparo_indirecto.fn_abo_create_recurso_queja_recurrente_incidental";
        public const string RecursoQuejaIncidentalOtrasAutoridadesCreate = "amparo_indirecto.fn_abo_create_recurso_queja_recurrente_incidental"; //SE REPITE
        public const string RecursoQuejaIncidentalUpdate = "amparo_indirecto.fn_abo_update_estado_procesal_recurso_queja_incidental";
        public const string RecursoQuejaIncidentalGetAll = "amparo_indirecto.fn_abo_get_all_recurso_queja_incidental";
        public const string RecursoQuejaIncidentalGetAllDisconnectInterno = "amparo_indirecto.fn_abo_get_all_autoridades_recurso_queja_incidental";

        public const string RecursoRevisionIncidentalGetAll = "amparo_indirecto.fn_abo_get_all_recurso_revision_incidental";
        public const string CreateRecursoRevisionIncidentalResponsable = "amparo_indirecto.fn_abo_create_recurso_revision_autoridades_incidental";
        public const string CreateRecursoRevisionIncidentalOtros = "amparo_indirecto.fn_abo_create_recurso_revision_recurrente_incidental";
        public const string CreateRecursoRevisionIncidentalQuejoso = "amparo_indirecto.fn_abo_create_recurso_revision_recurrente_incidental";
        public const string RecursoRevisionIncidentalGetAllDisconnectInterno = "amparo_indirecto.fn_abo_get_all_autoridades_recurso_revision_incidental";
        public const string RecursoRevisionIncidentalUpdate = "amparo_indirecto.fn_abo_update_estado_procesal_recurso_revision_incidental";

        public const string CreateIncidenteExceso = "amparo_indirecto.fn_abo_create_incidente_exceso_incidental";
        public const string GetIncidenteExceso = "amparo_indirecto.fn_abo_get_incidente_exceso_incidental";
        public const string GetAllIncidenteExceso = "amparo_indirecto.fn_abo_get_all_incidente_exceso_incidental";
        public const string UpdateIncidenteExceso = "amparo_indirecto.fn_abo_update_estado_procesal_incidente_exceso_incidental";
   

        #region Constitucional
        public const string GetCuadernoConstitucional = "amparo_indirecto.fn_abo_get_all_cuaderno_autoridades";
        public const string GetRecursoRevisionConstitucionalDisconnected = "amparo_indirecto.fn_abo_get_all_autoridades_recurso_revision_constitucional";
        public const string GetCuadernoConstitucionalRecurrente = "amparo_indirecto.fn_abo_get_all_cuaderno_autoridades_recurrente";

        public const string CreateSentenciaConstitucional = "amparo_indirecto.fn_abo_create_update_sentencia_constitucional";
        public const string GetAllSentenciaConstitucional = "amparo_indirecto.fn_abo_get_sentencia_constitucional";
        public const string UpdateSentenciaConstitucional = "amparo_indirecto.fn_abo_update_estado_procesal_sentencia_constitucional";

        public const string GetAllCumplimientoFalloProtector = "amparo_indirecto.fn_abo_get_all_cumplimiento_fallo_protector_constitucional";
        public const string CreateCumplimientoFalloProtector = "amparo_indirecto.fn_abo_create_cumplimiento_fallo_protector_constitucional";
        public const string UpdateCumplimientoFalloProtector = "amparo_indirecto.fn_abo_update_estado_procesal_cumplimiento_fallo_constitucional";

        public const string CreateAutorizadoInformeJustificado = "amparo_indirecto.fn_abo_insertar_autoridades_informe_justificado_ids";  //Eliminar
        public const string CreateInformeJustificado = "amparo_indirecto.fn_abo_create_update_informe_justificado_constitucional";
        //public const string CreateInformeJustificado = "amparo_indirecto.fn_abo_create_informe_justificado_2";
        public const string GetAllInformeJustificado = "amparo_indirecto.fn_abo_get_informe_justificado_constitucional";
        public const string UpdateInformeJustificado = "amparo_indirecto.fn_abo_update_estado_procesal_informe_justificado_constitucional";
        //public const string UpdateInformeJustificado = "amparo_indirecto.fn_abo_update_estado_procesal_informe_justificado_constitucional";

        //public const string CreateRecursoQuejaConstitucionalResponsable = "amparo_indirecto.fn_abo_update_recurso_queja_constitucional";
        public const string GetAllRecursoQuejaConstitucionalResponsable = "amparo_indirecto.fn_abo_get_all_recurso_queja_constitucional";
        //public const string CreateRecursoQuejaConstitucionalResponsable = "amparo_indirecto.fn_abo_update_recurso_queja_constitucional_2";
        public const string CreateRecursoQuejaConstitucionalResponsable = "amparo_indirecto.fn_abo_create_recurso_queja_autoridades_constitucional";
        public const string CreateRecursoQuejaConstitucionalOtros = "amparo_indirecto.fn_abo_create_recurso_queja_recurrente_constitucional";
        public const string CreateRecursoQuejaConstitucionalQuejoso = "amparo_indirecto.fn_abo_create_recurso_queja_recurrente_constitucional"; //Se repite
        public const string UpdateRecursoQuejaConstitucional = "amparo_indirecto.fn_abo_update_estado_procesal_recurso_queja_constitucional";

        public const string GetAllRecursoRevisionConstitucional = "amparo_indirecto.fn_abo_get_all_recurso_revision_constitucional";
        public const string CreateRecursoRevisionConstitucionalResponsable = "amparo_indirecto.fn_abo_create_recurso_revision_autoridades_constitucional";
        public const string CreateRecursoRevisionConstitucionalOtros = "amparo_indirecto.fn_abo_create_recurso_revision_recurrente_constitucional";
        public const string CreateRecursoRevisionConstitucionalQuejoso = "amparo_indirecto.fn_abo_create_recurso_revision_recurrente_constitucional"; //Se repite
        public const string UpdateRecursoRevisionConstitucional = "amparo_indirecto.fn_abo_update_estado_procesal_recurso_revision_constitucional";

        public const string GetAllRecursoInconformidad = "amparo_indirecto.fn_abo_get_recurso_inconformidad_constitucional";
        public const string CreateRecursoInconformidadConstitucional = "amparo_indirecto.fn_abo_create_recurso_inconformidad_constitucional";

        public const string GetAllRecursoReclamacion = "amparo_indirecto.fn_abo_get_recurso_reclamacion_constitucional";
        public const string CreateRecursoReclamacionConstitucional = "amparo_indirecto.fn_abo_create_recurso_reclamacion_constitucional";
        #endregion

        #endregion


        #region Administrador Global

        public const string BandejaAdministradorGlobalGetAll = "amparo_indirecto.fn_ag_bandeja_pendientes";
        public const string BandejaAdministradorGlobalGetAllCount = "amparo_indirecto.fn_ag_bandeja_pendientes_count";

        public const string GetByIdInformeJustificadoDescartar = "amparo_indirecto.fn_ag_read_descartar_informe_justificado_by_id";
        public const string UpdateDescartarInformeJustificado = "amparo_indirecto.fn_ag_descartar_informe_justificado_constitucional";
        public const string GetAllDescartarInformeJustificado = "amparo_indirecto.fn_ag_get_all_descartar_informe_justificado";

        #endregion


        #region Administrador Unidad Administrativa

        public const string BandejaAdministradorUnidadAdministrativaGetAll = "amparo_indirecto.fn_aua_bandeja_pendientes";
        public const string BandejaAdministradorUnidadAdministrativaGetAllCount = "amparo_indirecto.fn_aua_bandeja_pendientes_count";

        #endregion


        #region Documentos
        public const string AmparoIndirectoCreateDocumento = "amparo_indirecto.fn_create_documento";
        public const string AmparoIndirectoUpdateDocumento = "amparo_indirecto.fn_update_documentos_ai";
        public const string AmparoIndirectoDeleteDocumento = "amparo_indirecto.fn_update_documentos_ids";
        public const string AmparoIndirectoGetAllDocumento = "amparo_indirecto.fn_get_all_documentos_by_id";
        public const string AmparoIndirectoGetAllDocumentoSeccion = "amparo_indirecto.fn_get_historico_documento_by_juicio_seccion";
        public const string AmparoIndirectoGetDocumentoByIds = "amparo_indirecto.fn_get_documento_by_ids";
        public const string AmparoIndirectoGetDocumento = "amparo_indirecto.fn_get_documento_by_id";
        public const string AmparoIndirectoGetHistoricoDocumentoIdSeccion = "amparo_indirecto.fn_get_historico_documento_by_seccion";
        public const string AmparoIndirectoGetHistoricoCountDocumentoIdSeccion = "amparo_indirecto.fn_get_historico_documento_by_juicio_seccion_count";
        public const string AmparoIndirectoGetHistoricoCountDocumentoId = "amparo_indirecto.fn_get_historico_documento_by_id_count";
        public const string AmparoIndirectoGetHistoricoDocumentoId = "amparo_indirecto.fn_get_historico_documento_by_id";
        #endregion
    }
}
