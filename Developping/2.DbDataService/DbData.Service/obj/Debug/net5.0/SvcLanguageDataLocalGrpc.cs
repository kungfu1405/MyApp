// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcLanguageDataLocal.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace UserDb.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class LanguageDataLocalServices
  {
    static readonly string __ServiceName = "dbdata.LanguageDataLocalServices";

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

    static readonly grpc::Marshaller<global::UserDb.Protos.LanguageDataFilter> __Marshaller_dbdata_LanguageDataFilter = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.LanguageDataFilter.Parser));
    static readonly grpc::Marshaller<global::UserDb.Protos.LanguageDataStruct> __Marshaller_dbdata_LanguageDataStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.LanguageDataStruct.Parser));
    static readonly grpc::Marshaller<global::UserDb.Protos.ListLanguageDataResponse> __Marshaller_dbdata_ListLanguageDataResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.ListLanguageDataResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ResponseMessage> __Marshaller_dbdata_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ResponseMessage.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.IdRequest> __Marshaller_dbdata_IdRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.IdRequest.Parser));

    static readonly grpc::Method<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.LanguageDataStruct> __Method_Get = new grpc::Method<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.LanguageDataStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_LanguageDataFilter,
        __Marshaller_dbdata_LanguageDataStruct);

    static readonly grpc::Method<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.ListLanguageDataResponse> __Method_GetList = new grpc::Method<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.ListLanguageDataResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetList",
        __Marshaller_dbdata_LanguageDataFilter,
        __Marshaller_dbdata_ListLanguageDataResponse);

    static readonly grpc::Method<global::UserDb.Protos.LanguageDataStruct, global::DbData.Protos.ResponseMessage> __Method_Add = new grpc::Method<global::UserDb.Protos.LanguageDataStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_dbdata_LanguageDataStruct,
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
      get { return global::UserDb.Protos.SvcLanguageDataLocalReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of LanguageDataLocalServices</summary>
    [grpc::BindServiceMethod(typeof(LanguageDataLocalServices), "BindService")]
    public abstract partial class LanguageDataLocalServicesBase
    {
      public virtual global::System.Threading.Tasks.Task<global::UserDb.Protos.LanguageDataStruct> Get(global::UserDb.Protos.LanguageDataFilter request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::UserDb.Protos.ListLanguageDataResponse> GetList(global::UserDb.Protos.LanguageDataFilter request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Add(global::UserDb.Protos.LanguageDataStruct request, grpc::ServerCallContext context)
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
    public static grpc::ServerServiceDefinition BindService(LanguageDataLocalServicesBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_GetList, serviceImpl.GetList)
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_Delete, serviceImpl.Delete).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, LanguageDataLocalServicesBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.LanguageDataStruct>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_GetList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::UserDb.Protos.LanguageDataFilter, global::UserDb.Protos.ListLanguageDataResponse>(serviceImpl.GetList));
      serviceBinder.AddMethod(__Method_Add, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::UserDb.Protos.LanguageDataStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Add));
      serviceBinder.AddMethod(__Method_Delete, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ResponseMessage>(serviceImpl.Delete));
    }

  }
}
#endregion