az acr update -n pricestore --admin-enabled true

az acr credential show -n myveryownregistry \
    --query "join(' ', ['kubectl create secret docker-registry registrykey --docker-server myveryownregistry-on.azurecr.io', '--docker-username', username, '--docker-password', password, '--docker-email example@example.com'])" \
    --output tsv\
    | sh


az acr credential show -n pricestore --query "join(' ', ['kubectl create secret docker-registry registrykey --docker-server pricestore.azurecr.io', '--docker-username', pricestore, '--docker-password', password, '--docker-email rafael.miceli@hotmail.com'])" --output tsv | sh


kubectl create secret docker-registry registrykey --docker-server pricestore.azurecr.io --docker-username pricestore --docker-password password --docker-email rafael.miceli@hotmail.com

#Create repo with vsts cli
#vsts code repo create -i https://my-instance.visualstudio.com -p my-project --name my-repository