FROM mslavic/proto-gen-descriptor:latest as proto-gen-descriptor
WORKDIR /
ENTRYPOINT protoc -I$GOPATH/protobuf/src -I$GOPATH/googleapis -I/proto \
    --include_imports \
    --include_source_info \
    --descriptor_set_out=/descriptor/guide.pb \
    /proto/**/*.proto

FROM envoyproxy/envoy:v1.21.0 as front-proxy
WORKDIR /
CMD /usr/local/bin/envoy -c /etc/envoy.yaml -l debug --service-cluster front-proxy

FROM envoyproxy/envoy:v1.21.0 as front-proxy-validation
WORKDIR /
CMD /usr/local/bin/envoy -c /etc/envoy.yaml -l debug --mode validate