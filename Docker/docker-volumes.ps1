# Docu
## https://docs.docker.com/storage/volumes/

# list volumes
docker volume ls

# create new volume
docker volume create --name DataVolume1

# run container with mounted volume
docker run -ti --name=Container1 -v DataVolume1:/datavolume1 ubuntu

# edit file in volume 
echo "Edit in Container1" > /datavolume1/Example.txt

# run second container
# add :ro to mount as read-only
docker run -ti --name=Container2 --volumes-from Container1:ro ubuntu
docker run -ti --name=Container3 -v DataVolume1:/datavolume1:ro ubuntu

# edit file in volume 
echo "Edit in Container2" >> /datavolume1/Example.txt

# inspect
docker volume inspect DataVolume1
 
# Remove
docker volume rm DataVolume1
docker inspect Container1 # Mounts part

# start container and new volume at the same time:
docker run -d --name=nginxtest -v nginx-vol:/usr/share/nginx/html nginx:latest
docker container stop nginxtest
docker container rm nginxtest
docker volume rm nginx-vol