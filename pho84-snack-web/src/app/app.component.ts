import { Component } from '@angular/core';
import { PwaUpdateService } from "./services/pwa-update.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'pho84-snack-web';

  constructor(private update: PwaUpdateService) { }
}
