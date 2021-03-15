const electron = require('electron')

const { app, Tray, Menu } = electron

const path = require('path')

app.on('ready', _ => {

    new Tray(path.join('src', 'Flat-Enigma.ico'))
    // const tray = new Tray("F:/Projects/_GitHub/Exercise.Electron/menu-demo/src/Flat-Enigma.ico")

    const menu = Menu.buildFromTemplate([
        {
            label: 'Wow',
            click: _ => { console.log('clicked wow')}
        },
        {
            label: 'Awesome',
            click: _ => { console.log('clicked Awesome')}
        }
    ])

    tray.setContextMenu(menu)

    tray.setToolTip('Your amazing electron app :)')
})