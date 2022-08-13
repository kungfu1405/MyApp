// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcUserProfile.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DbData.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class UserProfileServices
  {
    static readonly string __ServiceName = "dbdata.UserProfileServices";

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
    static readonly grpc::Marshaller<global::DbData.Protos.UserProfileStruct> __Marshaller_dbdata_UserProfileStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.UserProfileStruct.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ResponseMessage> __Marshaller_dbdata_ResponseMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ResponseMessage.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.ListUserFollowResponse> __Marshaller_dbdata_ListUserFollowResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.ListUserFollowResponse.Parser));
    static readonly grpc::Marshaller<global::DbData.Protos.UserFollowStruct> __Marshaller_dbdata_UserFollowStruct = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.UserFollowStruct.Parser));

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.UserProfileStruct> __Method_Get = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.UserProfileStruct>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_UserProfileStruct);

    static readonly grpc::Method<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage> __Method_Add = new grpc::Method<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_dbdata_UserProfileStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage> __Method_Edit = new grpc::Method<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Edit",
        __Marshaller_dbdata_UserProfileStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse> __Method_GetFollowers = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFollowers",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_ListUserFollowResponse);

    static readonly grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse> __Method_GetFollowings = new grpc::Method<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFollowings",
        __Marshaller_dbdata_IdRequest,
        __Marshaller_dbdata_ListUserFollowResponse);

    static readonly grpc::Method<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage> __Method_AddFollow = new grpc::Method<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddFollow",
        __Marshaller_dbdata_UserFollowStruct,
        __Marshaller_dbdata_ResponseMessage);

    static readonly grpc::Method<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage> __Method_RemoveFollow = new grpc::Method<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RemoveFollow",
        __Marshaller_dbdata_UserFollowStruct,
        __Marshaller_dbdata_ResponseMessage);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::DbData.Protos.SvcUserProfileReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of UserProfileServices</summary>
    [grpc::BindServiceMethod(typeof(UserProfileServices), "BindService")]
    public abstract partial class UserProfileServicesBase
    {
      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.UserProfileStruct> Get(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Add(global::DbData.Protos.UserProfileStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> Edit(global::DbData.Protos.UserProfileStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ListUserFollowResponse> GetFollowers(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ListUserFollowResponse> GetFollowings(global::DbData.Protos.IdRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> AddFollow(global::DbData.Protos.UserFollowStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::DbData.Protos.ResponseMessage> RemoveFollow(global::DbData.Protos.UserFollowStruct request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for UserProfileServices</summary>
    public partial class UserProfileServicesClient : grpc::ClientBase<UserProfileServicesClient>
    {
      /// <summary>Creates a new client for UserProfileServices</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public UserProfileServicesClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UserProfileServices that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public UserProfileServicesClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected UserProfileServicesClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected UserProfileServicesClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::DbData.Protos.UserProfileStruct Get(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.UserProfileStruct Get(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.UserProfileStruct> GetAsync(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.UserProfileStruct> GetAsync(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage Add(global::DbData.Protos.UserProfileStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Add(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage Add(global::DbData.Protos.UserProfileStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Add, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddAsync(global::DbData.Protos.UserProfileStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddAsync(global::DbData.Protos.UserProfileStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Add, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage Edit(global::DbData.Protos.UserProfileStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Edit(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage Edit(global::DbData.Protos.UserProfileStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> EditAsync(global::DbData.Protos.UserProfileStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return EditAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> EditAsync(global::DbData.Protos.UserProfileStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
      }
      public virtual global::DbData.Protos.ListUserFollowResponse GetFollowers(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFollowers(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ListUserFollowResponse GetFollowers(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetFollowers, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ListUserFollowResponse> GetFollowersAsync(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFollowersAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ListUserFollowResponse> GetFollowersAsync(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetFollowers, null, options, request);
      }
      public virtual global::DbData.Protos.ListUserFollowResponse GetFollowings(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFollowings(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ListUserFollowResponse GetFollowings(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetFollowings, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ListUserFollowResponse> GetFollowingsAsync(global::DbData.Protos.IdRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFollowingsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ListUserFollowResponse> GetFollowingsAsync(global::DbData.Protos.IdRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetFollowings, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage AddFollow(global::DbData.Protos.UserFollowStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddFollow(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage AddFollow(global::DbData.Protos.UserFollowStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddFollow, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddFollowAsync(global::DbData.Protos.UserFollowStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddFollowAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> AddFollowAsync(global::DbData.Protos.UserFollowStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddFollow, null, options, request);
      }
      public virtual global::DbData.Protos.ResponseMessage RemoveFollow(global::DbData.Protos.UserFollowStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RemoveFollow(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.ResponseMessage RemoveFollow(global::DbData.Protos.UserFollowStruct request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_RemoveFollow, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> RemoveFollowAsync(global::DbData.Protos.UserFollowStruct request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RemoveFollowAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.ResponseMessage> RemoveFollowAsync(global::DbData.Protos.UserFollowStruct request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_RemoveFollow, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override UserProfileServicesClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UserProfileServicesClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(UserProfileServicesBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_Edit, serviceImpl.Edit)
          .AddMethod(__Method_GetFollowers, serviceImpl.GetFollowers)
          .AddMethod(__Method_GetFollowings, serviceImpl.GetFollowings)
          .AddMethod(__Method_AddFollow, serviceImpl.AddFollow)
          .AddMethod(__Method_RemoveFollow, serviceImpl.RemoveFollow).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, UserProfileServicesBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.UserProfileStruct>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_Add, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Add));
      serviceBinder.AddMethod(__Method_Edit, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.UserProfileStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.Edit));
      serviceBinder.AddMethod(__Method_GetFollowers, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse>(serviceImpl.GetFollowers));
      serviceBinder.AddMethod(__Method_GetFollowings, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.IdRequest, global::DbData.Protos.ListUserFollowResponse>(serviceImpl.GetFollowings));
      serviceBinder.AddMethod(__Method_AddFollow, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.AddFollow));
      serviceBinder.AddMethod(__Method_RemoveFollow, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DbData.Protos.UserFollowStruct, global::DbData.Protos.ResponseMessage>(serviceImpl.RemoveFollow));
    }

  }
}
#endregion