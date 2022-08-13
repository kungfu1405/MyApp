// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: svcComment.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DbData.Protos {
  /// <summary>
  /// service definition.
  /// </summary>
  public static partial class CommentServices
  {
    static readonly string __ServiceName = "dbdata.CommentServices";

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
    static readonly grpc::Marshaller<global::DbData.Protos.EmptyRequest> __Marshaller_dbdata_EmptyRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DbData.Protos.EmptyRequest.Parser));

    static readonly grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.EmptyRequest> __Method_Get = new grpc::Method<global::DbData.Protos.IdLangRequest, global::DbData.Protos.EmptyRequest>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_dbdata_IdLangRequest,
        __Marshaller_dbdata_EmptyRequest);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::DbData.Protos.SvcCommentReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for CommentServices</summary>
    public partial class CommentServicesClient : grpc::ClientBase<CommentServicesClient>
    {
      /// <summary>Creates a new client for CommentServices</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public CommentServicesClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for CommentServices that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public CommentServicesClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected CommentServicesClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected CommentServicesClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::DbData.Protos.EmptyRequest Get(global::DbData.Protos.IdLangRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::DbData.Protos.EmptyRequest Get(global::DbData.Protos.IdLangRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.EmptyRequest> GetAsync(global::DbData.Protos.IdLangRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::DbData.Protos.EmptyRequest> GetAsync(global::DbData.Protos.IdLangRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override CommentServicesClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new CommentServicesClient(configuration);
      }
    }

  }
}
#endregion