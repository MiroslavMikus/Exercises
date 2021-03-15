const fs = require('fs')
const exec = require('child_process').exec
const os = require('os')


let timer

function isDir(dir) {
    try{
        return fs.lstatSync(dir).isDirectory()
    }
    catch  {
        return false
    }
}

function checkGitStatus(dir) {
    console.log('running git status on:', dir)
    exec('git status', {
        cwd: dir
    }, (err, stdout, stderr)=>{
        // console.log("err",err)
        // console.log("stdout",stdout)
        // console.log("stderr",stderr)

        if (err) return setStatus('unknown')

        if (/nothing to commit/.test(stdout)) return setStatus('clean')

        return setStatus('dirty')
    })
}

function formatDir(dir){
    return /^~/.test(dir)
    ? os.homedir() + dir.substr(1).trim()
    : dir.trim()
}

function removeStatus(){
    let el = document.getElementById('status')
    el.classList.remove('unknown', 'clean', 'dirty')
    return el
}

function setStatus(status){
    const el = removeStatus()

    el.classList.add(status)
}

document.getElementById('input').addEventListener('keyup', evt =>{

    removeStatus()
    
    clearTimeout(timer)

    timer = setTimeout(_ => {
        
        const dir = formatDir(evt.target.value)

        if (isDir(dir)){
            checkGitStatus(dir)
        }
    }, 500)
})