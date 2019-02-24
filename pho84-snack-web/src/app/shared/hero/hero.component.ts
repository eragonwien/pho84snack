import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.css']
})
export class HeroComponent implements OnInit {
  @Input() image: string;
  @Input() title: string;
  @Input() subtitle: string;
  background: string;

  constructor() { }

  ngOnInit() {
    this.background = "url('/assets/images/compressed/" + this.image + ".jpg')";
  }

}
