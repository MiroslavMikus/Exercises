docker run -e AZP_POOL=Test \
	-e AZP_URL=https://dev.azure.com/scaliro \
	-e AZP_TOKEN=5sspzupzs4egajx2pr634vktyvy57qy7xhc2ptbmixwxzjd75fza \
	-e AZP_AGENT_NAME=ubuntu_agent \
	dockeragent:latest
