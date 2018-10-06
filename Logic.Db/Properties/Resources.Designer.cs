﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logic.Db.Properties {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Logic.Db.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Byte[].
        /// </summary>
        internal static byte[] Database {
            get {
                object obj = ResourceManager.GetObject("Database", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a database.db.
        /// </summary>
        internal static string DbFileName {
            get {
                return ResourceManager.GetString("DbFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a IPS.Logic.BD\Connection\.
        /// </summary>
        internal static string DbPath {
            get {
                return ResourceManager.GetString("DbPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a DELETE FROM Athlete WHERE DNI=@DNI.
        /// </summary>
        internal static string SQL_DELETE_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_DELETE_ATHLETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string SQL_DELETE_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_DELETE_COMPETITION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO Athlete VALUES(@DNI, @NAME, @SURNAME, @BIRTH_DATE, @GENDER).
        /// </summary>
        internal static string SQL_INSERT_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_INSERT_ATHLETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string SQL_INSERT_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_INSERT_COMPETITION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DNI, NAME, SURNAME, BIRTH_DATE, GENDER FROM Athlete.
        /// </summary>
        internal static string SQL_SELECT_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM Competition.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM Competition
        ///WHERE COMPETITION_STATUS=@STATUS.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_STATUS {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_STATUS", resourceCulture);
            }
        }

        internal static string SQL_SELECT_ATHLETES_TIMES {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_TIMES", resourceCulture);
            }
        }

        internal static string SQL_SELECT_ATHLETES_STATUS {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_STATUS", resourceCulture);
            }
        }

        internal static string SQL_INSERT_HAS_PARTICIPATED {
            get {
                return ResourceManager.GetString("SQL_INSERT_HAS_PARTICIPATED", resourceCulture);
            }
        }
    }
}
