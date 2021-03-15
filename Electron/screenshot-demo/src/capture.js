const electron = require('electron')
const path = require('path')
const fs = require('fs')

const { ipcRenderer: ipc , desktopCapturer, screen} = electron

function getMainSource(desktopCapturer, screen, done){
    const options = {types: ['screen'], thumbnailSize: screen.getPrimaryDisplay().workAreaSize }

    desktopCapturer.getSources(options, (err, sources) => {
        if (err) return console.log('Cannot capture screen:', err)
      
        const isMainSource = source => source.name === 'Entire screen' || source.name === 'Screen 1'

        done(sources.filter(isMainSource)[0])
    })
}

function onCapture(event, targetPath){
    getMainSource(desktopCapturer, screen, source => {
        const png = source.thumbnail.toPNG()
        const filePath = path.join(targetPath, `${new Date().getTime()}.png`)
        writeScreenshot(png, filePath)
    })
}

function writeScreenshot(png, filePath){
    console.log("saving screenshot to:", filePath)
    fs.writeFile(filePath, png, err => {
        if (err) return console.log('Failed to write screen:', err)
    })
}

ipc.on('capture', onCapture)
