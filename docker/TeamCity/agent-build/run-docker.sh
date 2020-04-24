#!/bin/bash

rm /var/run/docker.pid 2>/dev/null
service docker start
echo "Docker daemon started"

