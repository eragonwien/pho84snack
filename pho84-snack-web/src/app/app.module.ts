import { BrowserModule } from "@angular/platform-browser";
import { NgModule, LOCALE_ID } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { registerLocaleData } from "@angular/common";
import localeDE from "@angular/common/locales/de";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ServiceWorkerModule } from "@angular/service-worker";
import { environment } from "../environments/environment";
import { SharedModule } from "./shared.module";
import { HomeComponent } from "./home/home.component";
import { NavbarComponent } from "./shared/navbar/navbar.component";
import { HttpClientModule } from "@angular/common/http";
import { ContactComponent } from "./contact/contact.component";
import { HeroComponent } from "./shared/hero/hero.component";
import { NavbarBottomComponent } from "./shared/navbar-bottom/navbar-bottom.component";
import { GalleryComponent } from "./shared/gallery/gallery.component";
import { FooterComponent } from "./shared/footer/footer.component";
import { DisplayEvenPipe } from "./shared/display-even.pipe";
import { PriceListComponent } from "./shared/price-list/price-list.component";
import { NewsComponent } from "./shared/news/news.component";
import { DrinksComponent } from "./drinks/drinks.component";
import { FoodComponent } from "./food/food.component";
import { CCurrencyPipe } from "./shared/c-currency.pipe";
import { FeatureComponent } from "./shared/feature/feature.component";
import { PriceListBodyComponent } from "./shared/price-list-body/price-list-body.component";
import { CarouselFeatureComponent } from "./shared/carousel-feature/carousel-feature.component";

registerLocaleData(localeDE);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    ContactComponent,
    HeroComponent,
    NavbarBottomComponent,
    GalleryComponent,
    FooterComponent,
    DisplayEvenPipe,
    PriceListComponent,
    NewsComponent,
    DrinksComponent,
    FoodComponent,
    CCurrencyPipe,
    FeatureComponent,
    PriceListBodyComponent,
    CarouselFeatureComponent
  ],
  imports: [
    SharedModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ServiceWorkerModule.register("ngsw-worker.js", { enabled: environment.production }),
    BrowserAnimationsModule
  ],
  providers: [{ provide: LOCALE_ID, useValue: "de-DE" }],
  bootstrap: [AppComponent]
})
export class AppModule {}
