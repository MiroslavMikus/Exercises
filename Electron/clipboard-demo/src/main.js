const electron = require('electron')

const path = require('path')
const { app, clipboard, Menu, Tray, globalShortcut } = electron

const STACK_SIZE = 5
const ITEM_MAX_LENGTH = 20

function checkClipboardForChange(clipboard, onChange){
    let cache = clipboard.readText()
    let latest

    setInterval(_ => {
        latest = clipboard.readText()
        if (latest !== cache){
            cache = latest
            onChange(cache)
        }
    },1000)
}

function addToStack(item, stack){
    return [item].concat(stack.length >= STACK_SIZE ? stack.slice(0, stack.length -1) : stack)
}

function formatItem(item){
    return item && item.length > ITEM_MAX_LENGTH
    ? item.substr(0, ITEM_MAX_LENGTH) + '...'
    : item
}

function formatMenuTemplateForStack(clipboard, stack){
    return stack.map((item, i) => {
        return {
            label: `Copy ${formatItem(item)}`,
            click: _ => clipboard.writeText(item),
            accelerator: `Ctrl+Alt+${i+1}`
        }
    }).concat([quitLabel])
}

function registerShortcuts(globalShortcut, clipboard, stack){
    globalShortcut.unregisterAll()

    for (let i = 0; i < STACK_SIZE && i < stack.length; ++i){
        console.log("register", i+1, stack[i])
        globalShortcut.register(`Ctrl+Alt+${i+1}`,_ =>{
            clipboard.writeText(stack[i])
        })
    }
}

const quitLabel = {
    label: 'Quit',
    click: _ => app.quit()
}

app.on('ready',_ => {

    var tray = new Tray(path.join('src','Flat-Enigma.ico'))

    tray.setContextMenu(Menu.buildFromTemplate([
        {
            label: "<empty>",
            enabled: false
        },
        quitLabel
    ]))

    let stack = []

    checkClipboardForChange(clipboard, text => {

        if(!stack.includes(text)){

            stack = addToStack(text, stack)
            
            console.log('stack', stack)
            
            var template = formatMenuTemplateForStack(clipboard, stack)

            tray.setContextMenu(Menu.buildFromTemplate(template))
            
            registerShortcuts(globalShortcut, clipboard, stack)
        }
    })
})

app.on('will-quit', _ => {
    globalShortcut.unregisterAll()
})