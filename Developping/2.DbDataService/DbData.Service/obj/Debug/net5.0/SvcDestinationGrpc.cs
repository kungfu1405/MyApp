// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcDestination.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DbData.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class DestinationServices
  {
    static readonly string __ServiceName = "dbdata.DestinationServices";

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
    static readonly grpc::Marshaller<global::DbData.Protos.DestinationStruct> __Marshaller_dbdata_DestinationStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.DestinationStruct.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.IdRequest> __Marshaller_dbdata_IdRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.IdRequest.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.DestinationFilter> __Marshaller_dbdata_DestinationFilter = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.DestinationFilter.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ListDestinationResponse> __Marshaller_dbdata_ListDestinationResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ListDestinationResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ResponseMessage> __Marshaller_dbdata_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ResponseMessage.Parser));

    static readonly grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.DestinationStruct> __Method_Get = new grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.DestinationStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_IdLangRequest,
        __Marshaller_dbdata_DestinationStruct);

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.DestinationStruct> __Method_GetBy = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.DestinationStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetBy",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_DestinationStruct);

    static readonly grpc::Method<global::DbData.Protos.DestinationFilter, global::DbData.Protos.ListDestinationResponse> __Method_GetList = new grpc::Method<global::DbData.Protos.DestinationFilter, global::DbData.Protos.ListDestinationResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetList",
        __Marshaller_dbdata_DestinationFilter,
        __Marshaller_dbdata_ListDestinationResponse);

    static readonly grpc::Method<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage> __Method_Add = new grpc::Method<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_dbdata_DestinationStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage> __Method_Edit = new grpc::Method<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Edit",
        __Marshaller_dbdata_DestinationStruct,
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
      get { return global::DbData.Protos.SvcDestinationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of DestinationServices</summary>
    [grpc::BindServiceMethod(typeof(DestinationServices), "BindService")]
    public abstract partial class DestinationServicesBase
    {
      /// <summary>
      /// Id: RouteUri
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.DestinationStruct> Get(global::DbData.Protos.IdLangRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Id: Guid
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.DestinationStruct> GetBy(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ListDestinationResponse> GetList(global::DbData.Protos.DestinationFilter request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Add(global::DbData.Protos.DestinationStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Edit(global::DbData.Protos.DestinationStruct request, grpc::ServerCallContext context)
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
    public static grpc::ServerServiceDefinition BindService(DestinationServicesBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_GetBy, serviceImpl.GetBy)
          .AddMethod(__Method_GetList, serviceImpl.GetList)
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_Edit, serviceImpl.Edit)
          .AddMethod(__Method_Delete, serviceImpl.Delete).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, DestinationServicesBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdLangRequest, global::DbData.Protos.DestinationStruct>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_GetBy, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.DestinationStruct>(serviceImpl.GetBy));
      serviceBinder.AddMethod(__Method_GetList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.DestinationFilter, global::DbData.Protos.ListDestinationResponse>(serviceImpl.GetList));
      serviceBinder.AddMethod(__Method_Add, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Add));
      serviceBinder.AddMethod(__Method_Edit, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.DestinationStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Edit));
      serviceBinder.AddMethod(__Method_Delete, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ResponseMessage>(serviceImpl.Delete));
    }

  }
}
#endregion