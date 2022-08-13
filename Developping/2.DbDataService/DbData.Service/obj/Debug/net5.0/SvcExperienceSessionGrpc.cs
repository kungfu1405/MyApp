// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcExperienceSession.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DbData.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class ExperienceSessionServices
  {
    static readonly string __ServiceName = "dbdata.ExperienceSessionServices";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::DbData.Protos.IdLangRequest> __Marshaller_dbdata_IdLangRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.IdLangRequest.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ExperienceSessionStruct> __Marshaller_dbdata_ExperienceSessionStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ExperienceSessionStruct.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.IdRequest> __Marshaller_dbdata_IdRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.IdRequest.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ListExperienceSessionResponse> __Marshaller_dbdata_ListExperienceSessionResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ListExperienceSessionResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ExperienceSessionResponse> __Marshaller_dbdata_ExperienceSessionResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ExperienceSessionResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ResponseMessage> __Marshaller_dbdata_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ResponseMessage.Parser));

    static readonly grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ExperienceSessionStruct> __Method_Get = new grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ExperienceSessionStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_IdLangRequest,
        __Marshaller_dbdata_ExperienceSessionStruct);

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListExperienceSessionResponse> __Method_GetAllLanguage = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListExperienceSessionResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllLanguage",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_ListExperienceSessionResponse);

    static readonly grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ListExperienceSessionResponse> __Method_GetList = new grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ListExperienceSessionResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetList",
        __Marshaller_dbdata_IdLangRequest,
        __Marshaller_dbdata_ListExperienceSessionResponse);

    static readonly grpc::Method<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ExperienceSessionResponse> __Method_Add = new grpc::Method<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ExperienceSessionResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_dbdata_ExperienceSessionStruct,
        __Marshaller_dbdata_ExperienceSessionResponse);

    static readonly grpc::Method<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ResponseMessage> __Method_Edit = new grpc::Method<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Edit",
        __Marshaller_dbdata_ExperienceSessionStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ResponseMessage> __Method_Delete = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_ResponseMessage);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::DbData.Protos.SvcExperienceSessionReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ExperienceSessionServices</summary>
    [grpc::BindServiceMethod(typeof(ExperienceSessionServices), "BindService")]
    public abstract partial class ExperienceSessionServicesBase
    {
      /// <summary>
      /// Id: Session ID
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ExperienceSessionStruct> Get(global::DbData.Protos.IdLangRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Id: Session ID
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ListExperienceSessionResponse> GetAllLanguage(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Id: Experience ID
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ListExperienceSessionResponse> GetList(global::DbData.Protos.IdLangRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ExperienceSessionResponse> Add(global::DbData.Protos.ExperienceSessionStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Edit(global::DbData.Protos.ExperienceSessionStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Delete(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ExperienceSessionServicesBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_GetAllLanguage, serviceImpl.GetAllLanguage)
          .AddMethod(__Method_GetList, serviceImpl.GetList)
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_Edit, serviceImpl.Edit)
          .AddMethod(__Method_Delete, serviceImpl.Delete).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ExperienceSessionServicesBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ExperienceSessionStruct>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_GetAllLanguage, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ListExperienceSessionResponse>(serviceImpl.GetAllLanguage));
      serviceBinder.AddMethod(__Method_GetList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdLangRequest, global::DbData.Protos.ListExperienceSessionResponse>(serviceImpl.GetList));
      serviceBinder.AddMethod(__Method_Add, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ExperienceSessionResponse>(serviceImpl.Add));
      serviceBinder.AddMethod(__Method_Edit, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.ExperienceSessionStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Edit));
      serviceBinder.AddMethod(__Method_Delete, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ResponseMessage>(serviceImpl.Delete));
    }

  }
}
#endregion