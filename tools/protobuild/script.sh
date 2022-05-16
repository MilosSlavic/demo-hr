#!/bin/bash

usage()
{
    echo "script.sh proto_dir svc_name version [workdir]"
}

if [ $# -eq 0 ]; then
    echo "Missing arguments."
    usage
    exit 123
fi

if [ "$1" == "" ]; then
    echo "proto_dir not supplied."
    usage
    exit 22
fi

if [ "$2" == "" ]; then
    echo "svc_name not supplied."
    usage
    exit 33
fi

if [ "$3" == "" ]; then
    echo "version not supplied."
    usage
    exit 33
fi

workdir=$4
if [ "$4" == "" ]; then
    workdir=$(pwd)
fi

proto_dir=$1
svc_name=$2
version=$3
descriptor_dir=$workdir/descriptor
mkdir -p $descriptor_dir

mkdir -p $descriptor_dir/$svc_name/
descriptor_path=$descriptor_dir/$svc_name/$version.pb

df -h
echo "Descriptor path:"
echo $descriptor_path

 protoc -I$GOPATH/protobuf/src -I$GOPATH/googleapis -I$proto_dir \
    --include_imports \
    --include_source_info \
    --descriptor_set_out=$descriptor_path \
    $proto_dir/$svc_name/*.proto
