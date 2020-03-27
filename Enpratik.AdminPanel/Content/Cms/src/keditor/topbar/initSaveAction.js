import CSS_CLASS from '../constants/cssClass';
import getContent from '../getContent';
import ICON from '../constants/icon';

export default function () {
    let self = this;
    let options = self.options;
    let btnSave = self.btnSave = $(`<a href="#" title="${options.locale.save}" class="${CSS_CLASS.UI} ${CSS_CLASS.TOPBAR_BUTTON}">${ICON.SAVE}</a>`);
    btnSave.on('click', function (e) {
        console.log("save_click");
        e.preventDefault();
        
        let content = getContent.call(self);
        
        options.onSave.call(self, content);

    });
    
    self.topbarRight.append(btnSave);
};
