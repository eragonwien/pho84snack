import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from "./services/data.service";
import { PwaUpdateService } from "./services/pwa-update.service";

@NgModule({
  imports: [CommonModule]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        DataService,
        PwaUpdateService
      ]
    };
  }
}
