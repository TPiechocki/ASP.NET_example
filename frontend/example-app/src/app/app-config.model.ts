import { InjectionToken } from "@angular/core";

export interface IAppConfig {
  backendUrl: string;
}

export const APP_CONFIG = new InjectionToken<IAppConfig>('Injection token for application configuration');
