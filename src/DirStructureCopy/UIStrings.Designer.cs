﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirStructureCopy {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UIStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UIStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DirStructureCopy.UIStrings", typeof(UIStrings).Assembly);
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
        ///   Looks up a localized string similar to Operation was canceled..
        /// </summary>
        internal static string canceledResult {
            get {
                return ResourceManager.GetString("canceledResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception when proccessing directory &apos;{0}&apos; in &apos;{1}&apos;.
        /// </summary>
        internal static string dirProcessingException {
            get {
                return ResourceManager.GetString("dirProcessingException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string error {
            get {
                return ResourceManager.GetString("error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred..
        /// </summary>
        internal static string errorResult {
            get {
                return ResourceManager.GetString("errorResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Original size was {0} B.
        /// </summary>
        internal static string fileComment {
            get {
                return ResourceManager.GetString("fileComment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception when proccessing file &apos;{0}&apos; in &apos;{1}&apos;.
        /// </summary>
        internal static string fileProcessingException {
            get {
                return ResourceManager.GetString("fileProcessingException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source directory doesn&apos;t exist.
        /// </summary>
        internal static string sourceDirectoryDoesntExist {
            get {
                return ResourceManager.GetString("sourceDirectoryDoesntExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Directory structure was successfully copied..
        /// </summary>
        internal static string successResult {
            get {
                return ResourceManager.GetString("successResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception when proccessing zip file &apos;{0}&apos; in &apos;{1}&apos;.
        /// </summary>
        internal static string zipProcessingException {
            get {
                return ResourceManager.GetString("zipProcessingException", resourceCulture);
            }
        }
    }
}