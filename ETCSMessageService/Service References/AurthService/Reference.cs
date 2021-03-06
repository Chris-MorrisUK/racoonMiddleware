﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETCSMessageService.AurthService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SimpleRacoonResponse", Namespace="http://schemas.datacontract.org/2004/07/RacoonMiddleWare")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ETCSMessageService.AurthService.RacoonAurthorisationResponse))]
    public partial class SimpleRacoonResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AuthorisationOKField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Exception ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool StatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool AuthorisationOK {
            get {
                return this.AuthorisationOKField;
            }
            set {
                if ((this.AuthorisationOKField.Equals(value) != true)) {
                    this.AuthorisationOKField = value;
                    this.RaisePropertyChanged("AuthorisationOK");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Exception Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorField, value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RacoonAurthorisationResponse", Namespace="http://schemas.datacontract.org/2004/07/RacoonMiddleWare")]
    [System.SerializableAttribute()]
    public partial class RacoonAurthorisationResponse : ETCSMessageService.AurthService.SimpleRacoonResponse {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] TokenField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AurthService.IAurthenticateService")]
    public interface IAurthenticateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAurthenticateService/Authenticate", ReplyAction="http://tempuri.org/IAurthenticateService/AuthenticateResponse")]
        ETCSMessageService.AurthService.RacoonAurthorisationResponse Authenticate(string userName, string password, string stardogUser, string stardogPassword, System.Uri stardogServer, string stardogDatastore, string language);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAurthenticateService/Authenticate", ReplyAction="http://tempuri.org/IAurthenticateService/AuthenticateResponse")]
        System.Threading.Tasks.Task<ETCSMessageService.AurthService.RacoonAurthorisationResponse> AuthenticateAsync(string userName, string password, string stardogUser, string stardogPassword, System.Uri stardogServer, string stardogDatastore, string language);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAurthenticateServiceChannel : ETCSMessageService.AurthService.IAurthenticateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AurthenticateServiceClient : System.ServiceModel.ClientBase<ETCSMessageService.AurthService.IAurthenticateService>, ETCSMessageService.AurthService.IAurthenticateService {
        
        public AurthenticateServiceClient() {
        }
        
        public AurthenticateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AurthenticateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AurthenticateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AurthenticateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ETCSMessageService.AurthService.RacoonAurthorisationResponse Authenticate(string userName, string password, string stardogUser, string stardogPassword, System.Uri stardogServer, string stardogDatastore, string language) {
            return base.Channel.Authenticate(userName, password, stardogUser, stardogPassword, stardogServer, stardogDatastore, language);
        }
        
        public System.Threading.Tasks.Task<ETCSMessageService.AurthService.RacoonAurthorisationResponse> AuthenticateAsync(string userName, string password, string stardogUser, string stardogPassword, System.Uri stardogServer, string stardogDatastore, string language) {
            return base.Channel.AuthenticateAsync(userName, password, stardogUser, stardogPassword, stardogServer, stardogDatastore, language);
        }
    }
}
