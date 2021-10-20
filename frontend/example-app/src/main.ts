import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { API_BASE_URL } from 'projects/webapi-contract/src/lib/api-generated.contract';
import { APP_CONFIG, IAppConfig } from './app/app-config.model';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';


if (environment.production) {
  enableProdMode();
}

const jsonFile = `assets/config/local/config.json`;
const oReq = new XMLHttpRequest();
oReq.addEventListener('load', () => {
  if (oReq.status === 200) {
    const config = JSON.parse(oReq.responseText) as IAppConfig;
    platformBrowserDynamic([
      { provide: APP_CONFIG, useValue: config },
      { provide: API_BASE_URL, useValue: config['backendUrl'] }
    ]).bootstrapModule(AppModule, { preserveWhitespaces: true });
  } else {
    console.error(`Error while loading ${jsonFile}`);
  }
});

oReq.open('GET', jsonFile);
oReq.send();
