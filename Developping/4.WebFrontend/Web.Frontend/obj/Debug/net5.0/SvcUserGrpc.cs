// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcUser.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace UserDb.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class UserServices
  {
    static readonly string __ServiceName = "dbdata.UserServices";

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

    static readonly grpc::Marshaller<global::DbData.Protos.IdRequest> __Marshaller_dbdata_IdRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.IdRequest.Parser));
    static readonly grpc::Marshaller<global::UserDb.Protos.UserStruct> __Marshaller_dbdata_UserStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.UserStruct.Parser));
    static readonly grpc::Marshaller<global::UserDb.Protos.UserFilter> __Marshaller_dbdata_UserFilter = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.UserFilter.Parser));
    static readonly grpc::Marshaller<global::UserDb.Protos.ListUserResponse> __Marshaller_dbdata_ListUserResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserDb.Protos.ListUserResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ResponseMessage> __Marshaller_dbdata_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ResponseMessage.Parser));

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::UserDb.Protos.UserStruct> __Method_Get = new grpc::Method<global::DbData.Protos.IdRequest, global::UserDb.Protos.UserStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_UserStruct);

    static readonly grpc::Method<global::UserDb.Protos.UserFilter, global::UserDb.Protos.ListUserResponse> __Method_GetList = new grpc::Method<global::UserDb.Protos.UserFilter, global::UserDb.Protos.ListUserResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetList",
        __Marshaller_dbdata_UserFilter,
        __Marshaller_dbdata_ListUserResponse);

    static readonly grpc::Method<global::UserDb.Protos.UserStruct, global::DbData.Protos.ResponseMessage> __Method_Add = new grpc::Method<global::UserDb.Protos.UserStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_dbdata_UserStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::UserDb.Protos.UserStruct, global::DbData.Protos.ResponseMessage> __Method_Edit = new grpc::Method<global::UserDb.Protos.UserStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Edit",
        __Marshaller_dbdata_UserStruct,
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
      get { return global::UserDb.Protos.SvcUserReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for UserServices</summary>
    public partial class UserServicesClient : grpc::ClientBase<UserServicesClient>
    {
      /// <summary>Creates a new client for UserServices</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public UserServicesClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UserServices that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public UserServicesClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected UserServicesClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected UserServicesClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::UserDb.Protos.UserStruct Get(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::UserDb.Protos.UserStruct Get(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::UserDb.Protos.UserStruct> GetAsync(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::UserDb.Protos.UserStruct> GetAsync(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      public virtual global::UserDb.Protos.ListUserResponse GetList(global::UserDb.Protos.UserFilter request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetList(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::UserDb.Protos.ListUserResponse GetList(global::UserDb.Protos.UserFilter request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetList, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::UserDb.Protos.ListUserResponse> GetListAsync(global::UserDb.Protos.UserFilter request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetListAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::UserDb.Protos.ListUserResponse> GetListAsync(global::UserDb.Protos.UserFilter request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetList, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage Add(global::UserDb.Protos.UserStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Add(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage Add(global::UserDb.Protos.UserStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Add, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddAsync(global::UserDb.Protos.UserStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddAsync(global::UserDb.Protos.UserStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Add, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage Edit(global::UserDb.Protos.UserStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Edit(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage Edit(global::UserDb.Protos.UserStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> EditAsync(global::UserDb.Protos.UserStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return EditAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> EditAsync(global::UserDb.Protos.UserStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage Delete(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage Delete(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> DeleteAsync(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> DeleteAsync(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override UserServicesClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UserServicesClient(configuration);
      }
    }

  }
}
#endregion