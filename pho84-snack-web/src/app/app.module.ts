import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { SharedModule } from './shared.module';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { ContactComponent } from './contact/contact.component';
import { MenuComponent } from './menu/menu.component';
import { ProductComponent } from './product/product.component';
import { AboutComponent } from './about/about.component';
import { HomeFeaturedComponent } from './home/home-featured/home-featured.component';
import { HeroComponent } from './shared/hero/hero.component';
import { NavbarBottomComponent } from './shared/navbar-bottom/navbar-bottom.component';
import { GalleryComponent } from './shared/gallery/gallery.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HcardComponent } from './shared/hcard/hcard.component';
import { DisplayEvenPipe } from './shared/display-even.pipe';
import { CategoryBlockComponent } from './shared/category-block/category-block.component';
import { PriceListComponent } from './shared/price-list/price-list.component';
import { NewsComponent } from './shared/news/news.component';
import { DrinksComponent } from './drinks/drinks.component';
import { FoodComponent } from './food/food.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    ContactComponent,
    MenuComponent,
    ProductComponent,
    AboutComponent,
    HomeFeaturedComponent,
    HeroComponent,
    NavbarBottomComponent,
    GalleryComponent,
    FooterComponent,
    HcardComponent,
    DisplayEvenPipe,
    CategoryBlockComponent,
    PriceListComponent,
    NewsComponent,
    DrinksComponent,
    FoodComponent
  ],
  imports: [
    SharedModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
