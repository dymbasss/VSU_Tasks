import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({

    /*
    declarations: классы представлений (view classes), которые принадлежат модулю. Angular имеет три типа классов представлений: компоненты (components), директивы (directives), каналы (pipes)
    imports: другие модули, классы которых необходимы для шаблонов компонентов из текущего модуля
    bootstrap: корневой компонент, который вызывается по умолчанию при загрузке приложения 
    exports: набор классов представлений, которые должны использоваться в шаблонах компонентов из других модулей
    providers: классы, создающие сервисы, используемые модулем
    */

    imports: [BrowserModule, FormsModule, NgxPaginationModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent],
    exports: [],
    providers: []
})
export class AppModule { }