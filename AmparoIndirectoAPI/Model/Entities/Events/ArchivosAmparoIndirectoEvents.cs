using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.Entities.Events
{
    public class ArchivosAmparoIndirectoEvents
    {
        #region Crear registro documentos
        public static ArchivosAmparoIndirecto CreateDocumentoAmparoIndirecto(

                        int id_numero_asunto,
                        //int id_juicio_amparo, se modifico
                        int id_tipo_documento,
                        int id_seccion,
                        int id_unidad_administrativa,
                        string file_name,
                        string file_path,
                        string content_type,
                        string file_size,
                        int id_rol,
                        string usuario


        )
        {

            //Guard.ValidateStringEmpty(ref nombre, "Nombre documento");
            //Guard.ValidateStringEmpty(ref ruta, "Ruta documento");


            ArchivosAmparoIndirecto entity = new()
            {
                id_numero_asunto = id_numero_asunto,//se modifico id_juicio_asunto
                id_tipo_documento = id_tipo_documento,
                id_seccion = id_seccion,
                id_unidad_administrativa = id_unidad_administrativa,
                file_name = file_name,
                file_path = file_path,
                content_type = content_type,
                file_size = file_size,
                permanente = false,
                reemplazable = true,
                id_rol = id_rol,
                usuario = usuario
            };

            return entity;
        }
        #endregion

        #region Update registro documentos
        public static ArchivosAmparoIndirecto UpdateDocumento(

                        int id,
                        int id_numero_asunto, 
                       // int id_juicio_amparo, //se modifico
                        int id_tipo_documento,
                        int id_seccion,
                        int id_unidad_administrativa,
                        string file_name,
                        string file_path,
                        string content_type,
                        string file_size,
                        int id_rol,
                        string usuario,
                        string usuario_modificacion
        )
        {

            //Guard.ValidateStringEmpty(ref nombre, "Nombre documento");
            //Guard.ValidateStringEmpty(ref ruta, "Ruta documento");


            ArchivosAmparoIndirecto entity = new()
            {
                id = id,
                id_numero_asunto = id_numero_asunto,
                id_tipo_documento = id_tipo_documento,
                id_seccion = id_seccion,
                id_unidad_administrativa = id_unidad_administrativa,
                file_name = file_name,
                file_path = file_path,
                content_type = content_type,
                file_size = file_size,
                permanente = false,
                reemplazable = true,
                id_rol = id_rol,
                usuario = usuario,
                usuario_modificacion = usuario_modificacion
            };

            return entity;
        }
        #endregion

        public static void Delete(ref ArchivosAmparoIndirecto entity, string usuarioModificacion)
        {
            if (!entity.activo)
                throw new Exception("El documento ya se encuentra eliminado.");

            if (entity.permanente)
                throw new Exception("El documento no se puede eliminar.");

            entity.activo = false;
            //entity.usuario = "usuarioModificacion";
            entity.usuario = usuarioModificacion;
        }
    }
}
