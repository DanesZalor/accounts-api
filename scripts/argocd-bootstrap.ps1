kubectl create namespace argocd

# kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml 

# workaround if we encounter image pull backoffs

$imagesToPull = $(Get-Content .\argocd-install.yaml | findstr "image:" | Select-Object -Unique) -Replace '(image:)|(\s+)', ''

foreach($img in $imagesToPull)
{
    Write-Output "pulling $img"
    podman pull --tls-verify=$False $img
    Write-Output "loading $img into kind"
    ./KindLoad.ps1 $img
}

kubectl apply -n argocd -f .\argocd-install.yaml

kubectl -n argocd wait --for=condition=Ready=true pod --all

$argocd_password = ./Base64Decode.ps1 $(kubectl -n argocd get secret argocd-initial-admin-secret -o jsonpath="{.data.password}")

Write-Host "ArgoCD credentials:" 
Write-Host "    username: admin" 
Write-Host "    password: $argocd_password" 
Write-Host "now listening at localhost:8080" 

kubectl port-forward svc/argocd-server -n argocd 8080:443