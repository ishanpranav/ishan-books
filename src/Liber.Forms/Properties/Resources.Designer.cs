﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Liber.Forms.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Liber.Forms.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to account.
        /// </summary>
        internal static string Account {
            get {
                return ResourceManager.GetString("Account", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save changes.
        /// </summary>
        internal static string CancelCaption {
            get {
                return ResourceManager.GetString("CancelCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Companies.
        /// </summary>
        internal static string CompaniesJumpListCustomCategory {
            get {
                return ResourceManager.GetString("CompaniesJumpListCustomCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string ExceptionCaption {
            get {
                return ResourceManager.GetString("ExceptionCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon Icon {
            get {
                object obj = ResourceManager.GetObject("Icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The transaction does not balance. Please ensure that total amount entered in the debit column is equal to the total amount entered in the credit column..
        /// </summary>
        internal static string ImbalanceError {
            get {
                return ResourceManager.GetString("ImbalanceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a valid account..
        /// </summary>
        internal static string InvalidAccountError {
            get {
                return ResourceManager.GetString("InvalidAccountError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (None).
        /// </summary>
        internal static string NoAccount {
            get {
                return ResourceManager.GetString("NoAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are about to close the company file without saving your changes..
        /// </summary>
        internal static string WarningText {
            get {
                return ResourceManager.GetString("WarningText", resourceCulture);
            }
        }
    }
}
