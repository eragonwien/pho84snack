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
  @Input() size: string;
  background: string;
  heroClass: string;

  constructor() { }

  ngOnInit() {
    this.background = "url('/assets/images/compressed/" + this.image + ".jpg')";
    this.heroClass = "hero primary " + (this.size ? this.size : "is-large");
  }

}
